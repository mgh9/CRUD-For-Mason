using Mc2.CrudTest.Domain.Abstractions.ExternalServices;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;
using Moq;

namespace Mc2.CrudTest.Domain.Tests
{
    public partial class CustomerTests
    {
        public static IEnumerable<object[]> NullOrEmptyPhoneNumbers=>
                                                 new List<object[]>
                                                 {
                                                    new object[] { null },
                                                    new object[] { "" },  
                                                 };

        public static IEnumerable<object[]> NotNullButInvalidPhoneNumbers=>
                                                 new List<object[]>
                                                 {
                                                    new object[] { "+98" }, 
                                                    new object[] { "invalid-phone" },
                                                    new object[] { "1" },  
                                                    new object[] { "4,66" }, 
                                                    new object[] { "a.,6@345" },
                                                    new object[] { "1va,@" }, 
                                                    new object[] { "^%$gd&'" },   
                                                    new object[] { "@da" },   
                                                    new object[] { "+++565654465@a.com" }, 
                                                    new object[] { "-98936000" }, 
                                                    new object[] { "-989364726673" }, 
                                                 };

        public static IEnumerable<object[]> ValidPhoneNumbers=>
                                         new List<object[]>
                                         {
                                                    new object[] { "+989364726673" },
                                                    new object[] { "+543521448887" }, 
                                                    new object[] { "+16517803862" },
                                                    new object[] { "+12025550113" },
                                         };

        [Theory]
        [MemberData(nameof(NullOrEmptyPhoneNumbers))]
        public void CreateCustomer_ThrowsException_ForEmptyPhoneNumber(string emptyPhoneNumber)
        {
            // Arrange
            var phoneNumberValidatorMock = new Mock<IPhoneNumberValidator>();
            phoneNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(false);

            var bankAccountNumberValidatorMock = new Mock<IBankAccountNumberValidator>();
            bankAccountNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(false);

            var id = Guid.NewGuid();

            Assert.Throws<InvalidPhoneNumberException>(() => Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create(emptyPhoneNumber, phoneNumberValidatorMock.Object)
                , Email.Create("mahdi.ghardashpoor@gmail.com")
                , BankAccountNumber.Create("DE85500211205996587344", bankAccountNumberValidatorMock.Object)));
        }

        [Theory]
        [MemberData(nameof(NotNullButInvalidPhoneNumbers))]
        public void CreateCustomer_ThrowsException_ForIInvalidPhoneNumber(string invalidPhoneNumber)
        {
            // Arrange
            var phoneNumberValidatorMock = new Mock<IPhoneNumberValidator>();
            phoneNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(false);

            var bankAccountNumberValidatorMock = new Mock<IBankAccountNumberValidator>();
            bankAccountNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(false);

            var id = Guid.NewGuid();

            Assert.Throws<InvalidPhoneNumberException>(() => Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create(invalidPhoneNumber, phoneNumberValidatorMock.Object)
                , Email.Create("mahdi.ghardashpoor@gmail.com")
                , BankAccountNumber.Create("DE85500211205996587344", bankAccountNumberValidatorMock.Object)));
        }

        [Theory]
        [MemberData(nameof(ValidPhoneNumbers))]        
        public void CreateCustomer_DontThrowsException_ForValidPhoneNumber(string validPhoneNumber)
        {
            // Arrange
            var phoneNumberValidatorMock = new Mock<IPhoneNumberValidator>();
            phoneNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(true);

            var bankAccountNumberValidatorMock= new Mock<IBankAccountNumberValidator>();
            bankAccountNumberValidatorMock.Setup(v => v.IsValid(It.IsAny<string>(), out It.Ref<string?>.IsAny)).Returns(true);

            var id = Guid.NewGuid();

            Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create(validPhoneNumber, phoneNumberValidatorMock.Object)
                , Email.Create("mahdi.ghardashpoor@gmail.com")
                , BankAccountNumber.Create("DE85500211205996587344", bankAccountNumberValidatorMock.Object));
        }
    }
}
