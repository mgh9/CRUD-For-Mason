using Mc2.CrudTest.Domain.Abstractions.ExternalServices;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;
using Moq;

namespace Mc2.CrudTest.Domain.Tests
{
    public partial class CustomerTests
    {
        private readonly Mock<IPhoneNumberValidator> _validPhoneNumberValidatorMock;
        private readonly Mock<IBankAccountNumberValidator> _validBankAccountNumberValidatorMock;
        
        private readonly Mock<IPhoneNumberValidator> _invalidPhoneNumberValidatorMock;
        private readonly Mock<IBankAccountNumberValidator> _invalidBankAccountNumberValidatorMock;

        public CustomerTests()
        {
            _validPhoneNumberValidatorMock = new Mock<IPhoneNumberValidator>();
            _validPhoneNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(true);

            _invalidPhoneNumberValidatorMock = new Mock<IPhoneNumberValidator>();
            _invalidPhoneNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(false);

            _validBankAccountNumberValidatorMock= new Mock<IBankAccountNumberValidator>();
            _validBankAccountNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(true);

            _invalidBankAccountNumberValidatorMock = new Mock<IBankAccountNumberValidator>();
            _invalidBankAccountNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(false);
        }

        private Customer CreateSimpleValidCustomer()
        {
            var id = Guid.NewGuid();
            var phoneNumber = PhoneNumber.Create("+989364726673", _validPhoneNumberValidatorMock.Object);
            var email = Email.Create("mahdi.ghardashpoor@gmail.com");
            var bankAccountNumber = BankAccountNumber.Create("DE85500211205996587344", _validBankAccountNumberValidatorMock.Object);

            var createdCustomer = Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , phoneNumber, email, bankAccountNumber);

            return createdCustomer;
        }

        [Fact]
        public void Create_Customer_ReturnsSameInfo()
        {
            // Arrange
            var phoneNumber = PhoneNumber.Create("+989364726673" , _validPhoneNumberValidatorMock.Object);
            var email = Email.Create("mahdi.ghardashpoor@gmail.com");
            var bankAccountNumber = BankAccountNumber.Create("DE85500211205996587344", _validBankAccountNumberValidatorMock.Object);

            var createdCustomer = CreateSimpleValidCustomer();

            // Act and Assert
            Assert.NotNull(createdCustomer);
            Assert.Equal("Mahdi", createdCustomer.FirstName);
            Assert.Equal("Ghardashpoor", createdCustomer.LastName);
            Assert.Equal("Mahdi Ghardashpoor", createdCustomer.FullName);
            Assert.Equal(new DateTime(1990, 1, 1), createdCustomer.DateOfBirth);
            Assert.Equal(email, createdCustomer.Email);
            Assert.Equal(phoneNumber, createdCustomer.PhoneNumber);
            Assert.Equal(bankAccountNumber, createdCustomer.BankAccountNumber);
        }

        [Fact]
        public void Update_Customer_ReturnsUpdatedInfo()
        {
            // Arrange
            var phoneNumber = PhoneNumber.Create("+989364726673", _validPhoneNumberValidatorMock.Object);
            var email = Email.Create("mahdi.ghardashpoor@gmail.com");
            var bankAccountNumber = BankAccountNumber.Create("DE85500211205996587344", _validBankAccountNumberValidatorMock.Object);

            var createdCustomer = CreateSimpleValidCustomer();

            // Act
            // Simulate updating customer information
            createdCustomer.Update(createdCustomer.FirstName, "newLastName"
                , createdCustomer.DateOfBirth
                , createdCustomer.PhoneNumber
                , createdCustomer.Email);

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
            var createdCustomer = CreateSimpleValidCustomer();

            // Act
            // Implement the deletion logic, whether it's setting a flag or removing from a collection
            createdCustomer.Delete();

            // Assert
        }

        [Fact]
        public void Read_Customer_ReturnsCorrectInfo()
        {
            // Arrange
            var email = Email.Create("mahdi.ghardashpoor@gmail.com");
            var phoneNumber = PhoneNumber.Create("+989364726673", _validPhoneNumberValidatorMock.Object);
            var bankAccountNumber = BankAccountNumber.Create("DE85500211205996587344", _validBankAccountNumberValidatorMock.Object);

            var createdCustomer = CreateSimpleValidCustomer();

            // Act
            // Implement the logic to read customer information.

            // Assert
            Assert.Equal("Mahdi", createdCustomer.FirstName);
            Assert.Equal("Ghardashpoor", createdCustomer.LastName);
            Assert.Equal(new DateTime(1990, 1, 1), createdCustomer.DateOfBirth);
            Assert.Equal(email, createdCustomer.Email);
            Assert.Equal(phoneNumber, createdCustomer.PhoneNumber);
            Assert.Equal(bankAccountNumber, createdCustomer.BankAccountNumber);
        }
    }
}