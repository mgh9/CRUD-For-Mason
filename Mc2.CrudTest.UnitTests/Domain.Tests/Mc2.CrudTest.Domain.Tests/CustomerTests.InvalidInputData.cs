using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;

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

        [Theory]
        [MemberData(nameof(NullOrEmptyEmails))]
        public void CreateCustomer_ThrowsException_ForEmptyEmail(string emptyEmail)
        {
            Assert.Throws<ArgumentException>(() => Customer.Create("Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1), emptyEmail, "+989364726673", "123456"));
        }

        [Theory]
        [MemberData(nameof(NotNullButInvalidEmails))]
        public void CreateCustomer_ThrowsException_ForIInvalidEmail(string invalidEmail)
        {
            Assert.Throws<ArgumentException>(() => Customer.Create("Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1), invalidEmail, "+989364726673", "123456"));
        }
    }
}
