using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

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
            var id = Guid.NewGuid();

            Assert.Throws<ArgumentException>(() => Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create(emptyPhoneNumber)
                , Email.Create("mahdi.ghardashpoor@gmail.com")
                , BankAccountNumber.Create("DE85500211205996587344")));
        }

        [Theory]
        [MemberData(nameof(NotNullButInvalidPhoneNumbers))]
        public void CreateCustomer_ThrowsException_ForIInvalidPhoneNumber(string invalidPhoneNumber)
        {
            var id = Guid.NewGuid();

            Assert.Throws<ArgumentException>(() => Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create(invalidPhoneNumber)
                , Email.Create("mahdi.ghardashpoor@gmail.com")
                , BankAccountNumber.Create("DE85500211205996587344")));
        }

        [Theory]
        [MemberData(nameof(ValidPhoneNumbers))]        
        public void CreateCustomer_DontThrowsException_ForValidPhoneNumber(string validPhoneNumber)
        {
            var id = Guid.NewGuid();

            Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create(validPhoneNumber)                
                , Email.Create("mahdi.ghardashpoor@gmail.com")
                , BankAccountNumber.Create("DE85500211205996587344"));
        }
    }
}
