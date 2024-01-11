using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities
{
    public partial class Customer
    {
        public void Delete()
        {
            IsDeleted = true;
        }

        public static Customer UpdateCustomer(Customer customer, string newEmail, string newPhoneNumber, string newBankAccountNumber)
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

            // Check if the new bank accoutn number format is valid
            if (!IsValidBankAccountNumber(newBankAccountNumber))
            {
                throw new ArgumentException("Invalid bank account number");
            }

            // Update the customer's information
            customer.Email = Email.Create(newEmail);
            customer.PhoneNumber = PhoneNumber.Create(newPhoneNumber);
            customer.BankAccountNumber = BankAccountNumber.Create(newBankAccountNumber);

            return customer;
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return true;
        }

        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            return true;
        }

        private static bool IsValidBankAccountNumber(string bankAccountNumber)
        {
            if (string.IsNullOrWhiteSpace(bankAccountNumber))
            {
                return false;
            }

            return true;
        }
    }
}
