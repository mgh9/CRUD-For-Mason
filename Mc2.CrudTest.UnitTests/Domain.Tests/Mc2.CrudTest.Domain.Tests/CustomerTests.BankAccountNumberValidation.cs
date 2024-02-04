using Mc2.CrudTest.Domain.Abstractions.ExternalServices;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;
using Moq;

namespace Mc2.CrudTest.Domain.Tests
{
    public partial class CustomerTests
    {
        public static IEnumerable<object[]> NullOrEmptyBankAccountNumbers =>
                                                 new List<object[]>
                                                 {
                                                    new object[] { null },
                                                    new object[] { "" },
                                                 };

        public static IEnumerable<object[]> NotNullButInvalidBankAccountNumbers =>
                                                 new List<object[]>
                                                 {
                                                    new object[] { "+98936" },
                                                    new object[] { "invalid-bankaccount" },
                                                    new object[] { ".1" },
                                                    new object[] { "4,66" },
                                                    new object[] { "a.,6@345" },
                                                    new object[] { "1va,@" },
                                                    new object[] { "^%$gd&'" },
                                                    new object[] { "@da" },
                                                    new object[] { "+++565654465@a.com" },
                                                    new object[] { "-98936000" },
                                                    new object[] { "-954g" },
                                                 };

        public static IEnumerable<object[]> ValidBankAccountNumbers =>
                                         new List<object[]>
                                         {
                                                    new object[] { "DE85500211205996587344" },
                                                    new object[] { "IT63R0300203280822474291437" },
                                                    new object[] { "DE85702202005797818536" },
                                                    new object[] { "CH7189144466481455489" },
                                         };

        [Theory]
        [MemberData(nameof(NullOrEmptyBankAccountNumbers))]
        public void CreateCustomer_ThrowsException_ForEmptyBankAccountNumber(string emptyBankAccountNumber)
        {
            // Arrange
            var phoneNumberValidatorMock = new Mock<IPhoneNumberValidator>();
            phoneNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(true);

            var bankAccountNumberValidatorMock = new Mock<IBankAccountNumberValidator>();
            bankAccountNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(false);

            var id = Guid.NewGuid();

            Assert.Throws<InvalidBankAccountNumberException>(() => Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create("+989364726673", phoneNumberValidatorMock.Object)
                , Email.Create("mahdi.ghardashpoor@gmail.com")
                , BankAccountNumber.Create(emptyBankAccountNumber, bankAccountNumberValidatorMock.Object)));
        }

        [Theory]
        [MemberData(nameof(NotNullButInvalidBankAccountNumbers))]
        public void CreateCustomer_ThrowsException_ForIInvalidBankAccountNumber(string invalidBankAccountNumber)
        {
            // Arrange
            var phoneNumberValidatorMock = new Mock<IPhoneNumberValidator>();
            phoneNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(true);

            var bankAccountNumberValidatorMock = new Mock<IBankAccountNumberValidator>();
            bankAccountNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(false);

            var id = Guid.NewGuid();

            Assert.Throws<InvalidBankAccountNumberException>(() => Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create("+989364726673", phoneNumberValidatorMock.Object)
                , Email.Create("mahdi.ghardashpoor@gmail.com")
                , BankAccountNumber.Create(invalidBankAccountNumber, bankAccountNumberValidatorMock.Object)));
        }

        [Theory]
        [MemberData(nameof(ValidBankAccountNumbers))]
        public void CreateCustomer_DontThrowsException_ForValidBankAccountNumber(string validBankAccountNumber)
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
                , Email.Create("mahdi.ghardashpoor@gmail.com")
                , BankAccountNumber.Create(validBankAccountNumber, bankAccountNumberValidatorMock.Object));
        }
    }
}
