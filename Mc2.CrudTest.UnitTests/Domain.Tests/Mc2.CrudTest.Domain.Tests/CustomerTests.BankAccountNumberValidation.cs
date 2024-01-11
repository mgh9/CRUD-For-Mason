using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;

namespace Mc2.CrudTest.Domain.Tests
{
    public partial class CustomerTests
    {
        public static IEnumerable<object[]> NullOrEmptyBankAccountNumbers=>
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
                                                    new object[] { "3423434" },
                                                    new object[] { "6655546" }, 
                                                    new object[] { "23434344" }, 
                                         };

        [Theory]
        [MemberData(nameof(NullOrEmptyBankAccountNumbers))]
        public void CreateCustomer_ThrowsException_ForEmptyBankAccountNumber(string emptyBankAccountNumber)
        {
            Assert.Throws<ArgumentException>(() => Customer.Create("Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1), "mahdi.ghardashpoor@gmail.com","+989364726673" , emptyBankAccountNumber));
        }

        [Theory]
        [MemberData(nameof(NotNullButInvalidBankAccountNumbers))]
        public void CreateCustomer_ThrowsException_ForIInvalidBankAccountNumber(string invalidBankAccountNumber)
        {
            Assert.Throws<ArgumentException>(() => Customer.Create("Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1), "mahdi.ghardashpoor@gmail.com", "+989364726673", invalidBankAccountNumber));
        }

        [Theory]
        [MemberData(nameof(ValidBankAccountNumbers))]
        public void CreateCustomer_DontThrowsException_ForValidBankAccountNumber(string validBankAccountNumber)
        {
            Customer.Create("Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1), "mahdi.ghardashpoor@gmail.com", "+989364726673", validBankAccountNumber);
        }
    }
}
