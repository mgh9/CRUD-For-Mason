using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;

namespace Mc2.CrudTest.Domain.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void Create_Customer_ReturnsSameInfo()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "Mahdi",
                LastName = "Ghardashpoor",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = "mahdi.ghardashpoor@gmail.com",
                PhoneNumber = "123",
                BankAccountNumber = "12345"
            };

            // Act and Assert
            Assert.NotNull(customer);
            Assert.Equal("Mahdi", customer.FirstName);
            Assert.Equal("Ghardashpoor", customer.LastName);
            Assert.Equal(new DateTime(1990, 1, 1), customer.DateOfBirth);
            Assert.Equal("123", customer.PhoneNumber);
            Assert.Equal("12345", customer.BankAccountNumber);
            Assert.Equal(false, customer.IsDeleted);
        }

        [Fact]
        public void Update_Customer_ReturnsUpdatedInfo()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "Mahdi",
                LastName = "Ghardashpoor",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = "mahdi.ghardashpoor@gmail.com",
                PhoneNumber = "123",
                BankAccountNumber = "12345"
            };

            // Act
            // Simulate updating customer information
            customer.Email = "updated.email@gmail.com";
            customer.PhoneNumber = "1234567890";

            // Assert
            Assert.Equal("Mahdi", customer.FirstName);
            Assert.Equal("Ghardashpoor", customer.LastName);
            Assert.Equal(new DateTime(1990, 1, 1), customer.DateOfBirth);
            Assert.Equal("updated.email@gmail.com", customer.Email);
            Assert.Equal("1234567890", customer.PhoneNumber);
            Assert.Equal("12345", customer.BankAccountNumber); // Bank account number should remain unchanged
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
            var customer = new Customer
            {
                FirstName = "Mahdi",
                LastName = "Ghardashpoor",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = "Mahdi.Ghardashpoor@gmail.com",
                PhoneNumber = "+989364726673",
                BankAccountNumber = "123456"
            };

            // Act
            // Implement the logic to read customer information.

            // Assert
            Assert.Equal("Mahdi", customer.FirstName);
            Assert.Equal("Ghardashpoor", customer.LastName);
            Assert.Equal(new DateTime(1990, 1, 1), customer.DateOfBirth);
            Assert.Equal("Mahdi.Ghardashpoor@gmail.com", customer.Email);
            Assert.Equal("+989364726673", customer.PhoneNumber);
            Assert.Equal("123456", customer.BankAccountNumber);
            Assert.False(customer.IsDeleted);
        }
    }
}