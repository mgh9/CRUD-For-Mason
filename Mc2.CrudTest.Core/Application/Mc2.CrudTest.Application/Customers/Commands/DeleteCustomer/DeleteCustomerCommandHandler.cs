using System.Security.Principal;
using Mc2.CrudTest.Application.Abstractions.Commands;
using Mc2.CrudTest.Application.Abstractions.Repositories;
using Mc2.CrudTest.Domain.Abstractions.Guards;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;
using MediatR;

namespace Mc2.CrudTest.Application.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler : CommandHandler<DeleteCustomerCommand>
{
    private readonly IEventStreamRepository<Customer> _customerEventStreamRepository;

    public DeleteCustomerCommandHandler(IEventStreamRepository<Customer> customerEventStreamRepository, IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
        _customerEventStreamRepository = customerEventStreamRepository;
    }

    protected override async Task HandleAsync(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerEventStreamRepository.GetByIdAsync(request.Id, cancellationToken);
        customer= Guard.Against.NotFound(customer);

        customer.Delete();

        await _customerEventStreamRepository.SaveAsync(customer, cancellationToken);
        await UnitOfWork.CommitAsync(cancellationToken);
    }
}