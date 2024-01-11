using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Domain.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void Create_Customer_ReturnsSameInfo()
        {
            // Arrange
            var phoneNumber = PhoneNumber.Create("+989364726673");
            var email = Email.Create("mahdi.ghardashpoor@gmail.com");
            var bankAccountNumber = BankAccountNumber.Create("123456");

            var customer = new Customer
            {
                FirstName = "Mahdi",
                LastName = "Ghardashpoor",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = email,
                PhoneNumber = phoneNumber,
                BankAccountNumber = bankAccountNumber
            };

            // Act and Assert
            Assert.NotNull(customer);
            Assert.Equal("Mahdi", customer.FirstName);
            Assert.Equal("Ghardashpoor", customer.LastName);
            Assert.Equal("Mahdi Ghardashpoor", customer.FullName);
            Assert.Equal(new DateTime(1990, 1, 1), customer.DateOfBirth);
            Assert.Equal(email, customer.Email);
            Assert.Equal(phoneNumber, customer.PhoneNumber);
            Assert.Equal(bankAccountNumber, customer.BankAccountNumber);
            Assert.Equal(false, customer.IsDeleted);
        }

        [Fact]
        public void Update_Customer_ReturnsUpdatedInfo()
        {
            // Arrange
            var phoneNumber = PhoneNumber.Create("+989364726673");
            var email = Email.Create("mahdi.ghardashpoor@gmail.com");
            var bankAccountNumber = BankAccountNumber.Create("123456");

            var customer = new Customer
            {
                FirstName = "Mahdi",
                LastName = "Ghardashpoor",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = email,
                PhoneNumber = phoneNumber,
                BankAccountNumber = bankAccountNumber
            };

            // Act
            // Simulate updating customer information
            var newEmail = Email.Create("updated.email@gmail.com");
            customer.Email = newEmail;

            var newPhoneNumber = PhoneNumber.Create("1234567890");
            customer.PhoneNumber = newPhoneNumber;

            //var newBankAccountNumber= new BankAccountNumber("12345");
            //customer.BankAccountNumber= newBankAccountNumber;

            // Assert
            Assert.Equal("Mahdi", customer.FirstName);
            Assert.Equal("Ghardashpoor", customer.LastName);
            Assert.Equal(new DateTime(1990, 1, 1), customer.DateOfBirth);
            Assert.Equal(newEmail, customer.Email);
            Assert.Equal(newPhoneNumber, customer.PhoneNumber);
            //Assert.Equal(newBankAccountNumber, customer.BankAccountNumber); // Bank account number should remain unchanged
        }

        [Fact]
        public void Delete_Customer_SetsIsDeletedFlag()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "Mahdi",
                LastName = "Ghardashpoor",
                IsDeleted = false
            };

            // Act
            // Implement the deletion logic, whether it's setting a flag or removing from a collection
            customer.Delete();

            // Assert
            Assert.True(customer.IsDeleted);
        }

        [Fact]
        public void Read_Customer_ReturnsCorrectInfo()
        {
            // Arrange
            var email  = Email.Create("Mahdi.Ghardashpoor@gmail.com");
            var phoneNumber = PhoneNumber.Create("+989364726673");
            var bankAccountNumber = BankAccountNumber.Create("123456");

            var customer = new Customer
            {
                FirstName = "Mahdi",
                LastName = "Ghardashpoor",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = email,
                PhoneNumber = phoneNumber,
                BankAccountNumber = bankAccountNumber
            };

            // Act
            // Implement the logic to read customer information.

            // Assert
            Assert.Equal("Mahdi", customer.FirstName);
            Assert.Equal("Ghardashpoor", customer.LastName);
            Assert.Equal(new DateTime(1990, 1, 1), customer.DateOfBirth);
            Assert.Equal(email, customer.Email);
            Assert.Equal(phoneNumber, customer.PhoneNumber);
            Assert.Equal(bankAccountNumber, customer.BankAccountNumber);
            Assert.False(customer.IsDeleted);
        }
    }
}