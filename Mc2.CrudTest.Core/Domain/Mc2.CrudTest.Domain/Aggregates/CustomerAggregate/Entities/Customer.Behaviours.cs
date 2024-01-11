using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities
{
    public partial class Customer
    {
        private Customer(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber, string bankAccountNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = Email.Create(email);
            PhoneNumber = PhoneNumber.Create(phoneNumber);
            BankAccountNumber = BankAccountNumber.Create(bankAccountNumber);
        }

        public static Customer Create(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber, string bankAccountNumber)
        {
            return new Customer(firstName, lastName, dateOfBirth, email, phoneNumber, bankAccountNumber);
        }

        public void Update(string newFirstName, string newLastName, DateTime newDateOfBirth, string newEmail, string newPhoneNumber, string newBankAccountNumber)
        {
            // Update the customer's information
            FirstName = newFirstName;
            LastName = newLastName;
            DateOfBirth = newDateOfBirth;
            Email = Email.Create(newEmail);
            PhoneNumber = PhoneNumber.Create(newPhoneNumber);
            BankAccountNumber = BankAccountNumber.Create(newBankAccountNumber);
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
