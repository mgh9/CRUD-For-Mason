using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

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
            var id = Guid.NewGuid();

            Assert.Throws<ArgumentException>(() => Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create("+989364726673")
                , Email.Create(emptyEmail)
                , BankAccountNumber.Create("DE85500211205996587344")));
        }

        [Theory]
        [MemberData(nameof(NotNullButInvalidEmails))]
        public void CreateCustomer_ThrowsException_ForIInvalidEmail(string invalidEmail)
        {
            var id = Guid.NewGuid();

            Assert.Throws<ArgumentException>(() => Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create("+989364726673")
                , Email.Create(invalidEmail)
                , BankAccountNumber.Create("DE85500211205996587344")));
        }

        [Theory]
        [MemberData(nameof(ValidEmails))]
        public void CreateCustomer_DontThrowsException_ForValidEmail(string validEmail)
        {
            var id = Guid.NewGuid();

            Customer.Create(id, "Mahdi", "Ghardashpoor"
                , new DateTime(1990, 1, 1)
                , PhoneNumber.Create("+989364726673")
                , Email.Create(validEmail)
                , BankAccountNumber.Create("DE85500211205996587344"));
        }
    }
}
