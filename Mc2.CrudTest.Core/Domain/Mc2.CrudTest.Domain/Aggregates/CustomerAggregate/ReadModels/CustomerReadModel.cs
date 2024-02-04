using Mc2.CrudTest.Domain.Abstractions.Models;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ReadModels
{
    public class CustomerReadModel : BaseReadModel
    {
        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Email Email { get; set; }

        public DateTime DateOfBirth { get; set; }
        // TODO: can be a value object if needed
        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        public BankAccountNumber BankAccountNumber { get; set; }
    }
}
