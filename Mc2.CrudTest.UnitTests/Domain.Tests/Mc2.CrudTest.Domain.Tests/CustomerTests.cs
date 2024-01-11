using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Domain.Tests
{
    public class CustomerTests
    {
        private static Customer CreateSimpleCustomer()
        {
            var phoneNumber = PhoneNumber.Create("+989364726673");
            var email = Email.Create("mahdi.ghardashpoor@gmail.com");
            var bankAccountNumber = BankAccountNumber.Create("123456");

            var createdCustomer = Customer.Create("Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , email.Value, phoneNumber.Value, bankAccountNumber.Value);

            return createdCustomer;
        }

        [Fact]
        public void Create_Customer_ReturnsSameInfo()
        {
            // Arrange
            var phoneNumber = PhoneNumber.Create("+989364726673");
            var email = Email.Create("mahdi.ghardashpoor@gmail.com");
            var bankAccountNumber = BankAccountNumber.Create("123456");

            var createdCustomer = CreateSimpleCustomer();

            // Act and Assert
            Assert.NotNull(createdCustomer);
            Assert.Equal("Mahdi", createdCustomer.FirstName);
            Assert.Equal("Ghardashpoor", createdCustomer.LastName);
            Assert.Equal("Mahdi Ghardashpoor", createdCustomer.FullName);
            Assert.Equal(new DateTime(1990, 1, 1), createdCustomer.DateOfBirth);
            Assert.Equal(email, createdCustomer.Email);
            Assert.Equal(phoneNumber, createdCustomer.PhoneNumber);
            Assert.Equal(bankAccountNumber, createdCustomer.BankAccountNumber);
            Assert.Equal(false, createdCustomer.IsDeleted);
        }

        [Fact]
        public void Update_Customer_ReturnsUpdatedInfo()
        {
            // Arrange
            var phoneNumber = PhoneNumber.Create("+989364726673");
            var email = Email.Create("mahdi.ghardashpoor@gmail.com");
            var bankAccountNumber = BankAccountNumber.Create("123456");

            var createdCustomer = CreateSimpleCustomer();

            // Act
            // Simulate updating customer information
            createdCustomer.Update(createdCustomer.FirstName
                , "newLastName", createdCustomer.DateOfBirth
                , createdCustomer.Email.Value, createdCustomer.PhoneNumber.Value, createdCustomer.BankAccountNumber.Value);

            // Assert
            Assert.Equal("Mahdi", createdCustomer.FirstName);
            Assert.Equal("newLastName", createdCustomer.LastName);
            Assert.Equal(new DateTime(1990, 1, 1), createdCustomer.DateOfBirth);
            Assert.Equal(email, createdCustomer.Email);
            Assert.Equal(phoneNumber, createdCustomer.PhoneNumber);
            Assert.Equal(bankAccountNumber, createdCustomer.BankAccountNumber);
        }

        [Fact]
        public void Delete_Customer_SetsIsDeletedFlag()
        {
            // Arrange
            var createdCustomer = CreateSimpleCustomer();

            // Act
            // Implement the deletion logic, whether it's setting a flag or removing from a collection
            createdCustomer.Delete();

            // Assert
            Assert.True(createdCustomer.IsDeleted);
        }

        [Fact]
        public void Read_Customer_ReturnsCorrectInfo()
        {
            // Arrange
            var email = Email.Create("mahdi.ghardashpoor@gmail.com");
            var phoneNumber = PhoneNumber.Create("+989364726673");
            var bankAccountNumber = BankAccountNumber.Create("123456");

            var createdCustomer = CreateSimpleCustomer();

            // Act
            // Implement the logic to read customer information.

            // Assert
            Assert.Equal("Mahdi", createdCustomer.FirstName);
            Assert.Equal("Ghardashpoor", createdCustomer.LastName);
            Assert.Equal(new DateTime(1990, 1, 1), createdCustomer.DateOfBirth);
            Assert.Equal(email, createdCustomer.Email);
            Assert.Equal(phoneNumber, createdCustomer.PhoneNumber);
            Assert.Equal(bankAccountNumber, createdCustomer.BankAccountNumber);
            Assert.False(createdCustomer.IsDeleted);
        }
    }
}