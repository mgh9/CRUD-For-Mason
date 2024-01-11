using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities
{
    public class Customer
    {
        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public PhoneNumber PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Email Email { get; set; }
        public BankAccountNumber BankAccountNumber { get; set; }
        public bool IsDeleted { get; set; }

        public void Delete()
        {
            IsDeleted = true;
        }

        public static Customer UpdateCustomer(Customer customer, string newEmail, string newPhoneNumber)
        {
            // Check if the new email format is valid
            if (!IsValidEmail(newEmail))
            {
                throw new ArgumentException("Invalid email format");
            }

            // Check if the new phone number format is valid
            if (!IsValidPhoneNumber(newPhoneNumber))
            {
                throw new ArgumentException("Invalid phone number format");
            }

            // Update the customer's information
            customer.Email = new Email(newEmail);
            customer.PhoneNumber = new PhoneNumber(newPhoneNumber);

            return customer;
        }

        private static bool IsValidEmail(string email)
        {
            return false;
        }

        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            return false;
        }
    }
}
