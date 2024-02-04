using Mc2.CrudTest.Application.Abstractions.Commands;
using Mc2.CrudTest.Application.Abstractions.Repositories;
using Mc2.CrudTest.Domain.Abstractions.ExternalServices;
using Mc2.CrudTest.Domain.Abstractions.Guards;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ReadModels;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : CommandHandler<UpdateCustomerCommand>
    {
        private readonly IReadModelRepository<CustomerReadModel> _customerReadModelRepository;
        private readonly IEventStreamRepository<Customer> _customerEventStreamRepository;

        private readonly IPhoneNumberValidator _phoneNumberValidator;

        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork
                                            , IReadModelRepository<CustomerReadModel> customerReadModelRepository
                                            , IEventStreamRepository<Customer> customerEventStreamRepository
                                            , IPhoneNumberValidator phoneNumberValidator)
            : base(unitOfWork)
        {
            _customerReadModelRepository = customerReadModelRepository;
            _customerEventStreamRepository = customerEventStreamRepository;
            _phoneNumberValidator = phoneNumberValidator;
        }

        protected override async Task HandleAsync(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerEventStreamRepository.GetByIdAsync(request.Id, cancellationToken);
            customer = Guard.Against.NotFound(customer);

            await CheckForExistingCustomerExceptHimselfAsync(request);

            var aNewPhoneNumber = PhoneNumber.Create(request.NewPhoneNumber, _phoneNumberValidator);
            var aNewEmail = Email.Create(request.NewEmail);

            customer.Update(request.NewFirstName, request.NewLastName, request.NewDateOfBirth, aNewPhoneNumber, aNewEmail);

            await _customerEventStreamRepository.SaveAsync(customer, cancellationToken);
            await UnitOfWork.CommitAsync(cancellationToken);

            return;
        }

        private async Task CheckForExistingCustomerExceptHimselfAsync(UpdateCustomerCommand model)
        {
            var customerWithSameInfoExceptHimSelf = await _customerReadModelRepository
                                                                .GetAsync(e => e.Id != model.Id
                                                                            && ((e.Email.Value == model.NewEmail) || (e.FirstName == model.NewFirstName && e.LastName == model.NewLastName && e.DateOfBirth.Date == model.NewDateOfBirth.Date)));

            var isThereAny = customerWithSameInfoExceptHimSelf.FirstOrDefault() is not null;
            if (isThereAny)
            {
                throw new CustomerAlreadyExistException(model.NewEmail, $"Customer already exists with the same Email address: `{model.NewEmail}`");
            }
        }
    }
}