using Mc2.CrudTest.Application.Abstractions.Commands;
using Mc2.CrudTest.Application.Abstractions.Repositories;
using Mc2.CrudTest.Domain.Abstractions.ExternalServices;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ReadModels;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : CommandHandler<CreateCustomerCommand>
    {
        private readonly IReadModelRepository<CustomerReadModel> _customerReadModelRepository;
        private readonly IEventStreamRepository<Customer> _customerEventStreamRepository;

        private readonly IPhoneNumberValidator _phoneNumberValidator;
        private readonly IBankAccountNumberValidator _bankAccountNumberValidator;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork
                                            , IReadModelRepository<CustomerReadModel> customerReadModelRepository
                                            , IEventStreamRepository<Customer> customerEventStreamRepository
                                            , IPhoneNumberValidator phoneNumberValidator
                                            , IBankAccountNumberValidator bankAccountNumberValidator)
            : base(unitOfWork)
        {
            _customerReadModelRepository = customerReadModelRepository;
            _customerEventStreamRepository = customerEventStreamRepository;
            _phoneNumberValidator = phoneNumberValidator;
            _bankAccountNumberValidator = bankAccountNumberValidator;
        }

        protected override async Task HandleAsync(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var aPhoneNumber = PhoneNumber.Create(request.PhoneNumber, _phoneNumberValidator);
            var anEmail = Email.Create(request.Email);
            var aBankAccountNumber = BankAccountNumber.Create(request.BankAccountNumber, _bankAccountNumberValidator);

            await CheckForExistingCustomerAsync(request);

            var customer = Customer.Create(request.Id
                                            , request.FirstName
                                            , request.LastName
                                            , request.DateOfBirth
                                            , aPhoneNumber
                                            , anEmail
                                            , aBankAccountNumber);

            await _customerEventStreamRepository.SaveAsync(customer, cancellationToken);
            await UnitOfWork.CommitAsync(cancellationToken);
            // This will save the Customer and CustomerReadModel in the same transaction

            return;
        }

        private async Task CheckForExistingCustomerAsync(CreateCustomerCommand model)
        {
            var customerWithSameInfo = await _customerReadModelRepository.GetAsync(e => (e.Email.Value == model.Email)
                                                                                || (e.FirstName == model.FirstName && e.LastName == model.LastName && e.DateOfBirth.Date == model.DateOfBirth.Date));

            var isThereAny = customerWithSameInfo.FirstOrDefault() is not null;
            if (isThereAny)
            {
                throw new CustomerAlreadyExistException(model.Email, $"Customer already exists with the same Email address: `{model.Email}`");
            }
        }
    }
}