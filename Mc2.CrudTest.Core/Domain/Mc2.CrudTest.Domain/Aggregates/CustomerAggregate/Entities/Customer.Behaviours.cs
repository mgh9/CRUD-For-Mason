using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Events;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities
{
    public partial class Customer
    {
        public void Update(string? newFirstName, string newLastName, DateTime newDateOfBirth, PhoneNumber newPhoneNumber, Email newEmail)
        {
            FirstName = (newFirstName ?? string.Empty).Trim();
            LastName = newLastName ?? throw new ArgumentNullException(newLastName);
            PhoneNumber = newPhoneNumber ?? throw new ArgumentNullException(nameof(newPhoneNumber));
            Email = newEmail ?? throw new ArgumentNullException(nameof(newEmail));

            if (IsValidBirthDate(newDateOfBirth))
            {
                throw new ArgumentException("Invalid date of birth");
            }

            AddEvent(new CustomerUpdatedDomainEvent(Id, newFirstName, newLastName, newDateOfBirth, newPhoneNumber, newEmail, DateTime.UtcNow));
        }

        private static bool IsValidBirthDate(DateTime newDateOfBirth)
        {
            return newDateOfBirth > DateTime.Now || newDateOfBirth < DateTime.Now.AddYears(-120);
        }

        public void Delete()
        {
            AddEvent(new CustomerDeletedDomainEvent(Id, DateTime.UtcNow));
        }
    }
}
