using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Events;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities
{
    public partial class Customer
    {
        internal void Apply(CustomerCreatedDomainEvent @event)
        {
            Id = @event.Id;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            Email = @event.Email;
            PhoneNumber = @event.PhoneNumber;
            BankAccountNumber = @event.BankAccountNumber;
            DateOfBirth = @event.DateOfBirth;
        }

        internal void Apply(CustomerDeletedDomainEvent @event)
        {
            Id = @event.Id;
        }

        internal void Apply(CustomerUpdatedDomainEvent @event)
        {
            Id = @event.Id;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            Email = @event.Email;
            PhoneNumber = @event.PhoneNumber;
            DateOfBirth = @event.DateOfBirth;
        }
    }
}
