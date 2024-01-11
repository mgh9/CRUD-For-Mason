using Mc2.CrudTest.Domain.Abstractions.ExternalServices;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;
using Moq;

namespace Mc2.CrudTest.Domain.Tests
{
    public partial class CustomerTests
    {
        public static IEnumerable<object[]> NullOrEmptyEmails =>
                                                 new List<object[]>
                                                 {
                                                    new object[] { null },
                                                    new object[] { "" },
                                                 };

        public static IEnumerable<object[]> NotNullButInvalidEmails =>
                                                 new List<object[]>
                                                 {
                                                    new object[] { "invalid-email" },
                                                    new object[] { "a.com" },
                                                    new object[] { "a.com," },
                                                    new object[] { "a.com,@" },
                                                    new object[] { "a,@" },
                                                    new object[] { "a@" },
                                                    new object[] { "@a" },
                                                    new object[] { "@a.com" },
                                                    new object[] { "@a.com" },
                                                 };

        public static IEnumerable<object[]> ValidEmails =>
                                         new List<object[]>
                                         {
                                                    new object[] { "valid-email@x.com" },
                                                    new object[] { "a@b.com" },
                                                    new object[] { "1@2.com" },
                                                    new object[] { "aasdasdsadasd@g.gov" },
                                                    new object[] { "mahdi.ghardashpoor@gmail.com" },
                                         };

        [Theory]
        [MemberData(nameof(NullOrEmptyEmails))]
        public void CreateCustomer_ThrowsException_ForEmptyEmail(string emptyEmail)
        {
            // Arrange
            var phoneNumberValidatorMock = new Mock<IPhoneNumberValidator>();
            phoneNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(true);

            var bankAccountNumberValidatorMock = new Mock<IBankAccountNumberValidator>();
            bankAccountNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(true);

            var id = Guid.NewGuid();

            Assert.Throws<InvalidEmailException>(() => Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create("+989364726673",phoneNumberValidatorMock.Object)
                , Email.Create(emptyEmail)
                , BankAccountNumber.Create("DE85500211205996587344", bankAccountNumberValidatorMock.Object)));
        }

        [Theory]
        [MemberData(nameof(NotNullButInvalidEmails))]
        public void CreateCustomer_ThrowsException_ForIInvalidEmail(string invalidEmail)
        {
            // Arrange
            var phoneNumberValidatorMock = new Mock<IPhoneNumberValidator>();
            phoneNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(true);

            var bankAccountNumberValidatorMock = new Mock<IBankAccountNumberValidator>();
            bankAccountNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(true);

            var id = Guid.NewGuid();

            Assert.Throws<InvalidEmailException>(() => Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create("+989364726673", phoneNumberValidatorMock.Object)
                , Email.Create(invalidEmail)
                , BankAccountNumber.Create("DE85500211205996587344", bankAccountNumberValidatorMock.Object)));
        }

        [Theory]
        [MemberData(nameof(ValidEmails))]
        public void CreateCustomer_DontThrowsException_ForValidEmail(string validEmail)
        {
            // Arrange
            var phoneNumberValidatorMock = new Mock<IPhoneNumberValidator>();
            phoneNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(true);

            var bankAccountNumberValidatorMock = new Mock<IBankAccountNumberValidator>();
            bankAccountNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(true);

            var id = Guid.NewGuid();

            Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create("+989364726673", phoneNumberValidatorMock.Object)
                , Email.Create(validEmail)
                , BankAccountNumber.Create("DE85500211205996587344", bankAccountNumberValidatorMock.Object));
        }
    }
}
