using Mc2.CrudTest.Application.Abstractions.Repositories;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Events;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ReadModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Application.Customers.DomainEventHandlers
{
    public sealed class CustomerReadModelHandler
                        : INotificationHandler<CustomerCreatedDomainEvent>
                        , INotificationHandler<CustomerUpdatedDomainEvent>
                        , INotificationHandler<CustomerDeletedDomainEvent>
    {
        private readonly IReadModelRepository<CustomerReadModel> _customerReadModelRepository;
        private readonly ILogger<CustomerReadModelHandler> _logger;

        public CustomerReadModelHandler(IReadModelRepository<CustomerReadModel> customerReadModelRepository, ILogger<CustomerReadModelHandler> logger)
        {
            _customerReadModelRepository = customerReadModelRepository;
            _logger = logger;
        }

        public Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a CustomerReadModel...");
            var aCustomer = new CustomerReadModel()
            {
                Id = notification.Id,
                FirstName = notification.FirstName,
                LastName = notification.LastName,
                DateOfBirth = notification.DateOfBirth,
                PhoneNumber = notification.PhoneNumber,
                Email = notification.Email,
                BankAccountNumber = notification.BankAccountNumber,
            };

            _customerReadModelRepository.Add(aCustomer);
            return Task.CompletedTask;
        }

        public Task Handle(CustomerDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting the CustomerReadModel...");
            var theCustomer = new CustomerReadModel()
            {
                Id = notification.Id
            };

            _customerReadModelRepository.Delete(theCustomer);
            return Task.CompletedTask;
        }

        public Task Handle(CustomerUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating the CustomerReadModel...");
            var theCustomer = new CustomerReadModel()
            {
                Id = notification.Id,
                FirstName = notification.FirstName,
                LastName = notification.LastName,
                DateOfBirth = notification.DateOfBirth,
                PhoneNumber = notification.PhoneNumber,
                Email = notification.Email,
            };

            _customerReadModelRepository.Update(theCustomer);
            return Task.CompletedTask;
        }
    }
}
