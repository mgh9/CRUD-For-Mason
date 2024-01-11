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
                                                    new object[] { "+98936" }, 
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
                                         };


        [Theory]
        [MemberData(nameof(NullOrEmptyPhoneNumbers))]
        public void CreateCustomer_ThrowsException_ForEmptyPhoneNumbers(string emptyPhoneNumber)
        {
            Assert.Throws<ArgumentException>(() => Customer.Create("Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1), "mahdi.ghardashpoor@gmail.com", emptyPhoneNumber, "123456"));
        }

        [Theory]
        [MemberData(nameof(NotNullButInvalidPhoneNumbers))]
        public void CreateCustomer_ThrowsException_ForIInvalidPhoneNumber(string invalidPhoneNumber)
        {
            Assert.Throws<ArgumentException>(() => Customer.Create("Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1), "mahdi.ghardashpoor@gmail.com", invalidPhoneNumber, "123456"));
        }

        [Theory]
        [MemberData(nameof(ValidPhoneNumbers))]
        public void CreateCustomer_DontThrowsException_ForValidPhoneNumber(string validPhoneNumber)
        {
            Customer.Create("Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1), "mahdi.ghardashpoor@gmail.com", validPhoneNumber, "123456");
        }
    }
}
