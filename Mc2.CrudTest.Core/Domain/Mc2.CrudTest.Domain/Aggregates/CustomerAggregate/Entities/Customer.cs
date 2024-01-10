namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities
{
    public class Customer
    {
        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public bool IsDeleted { get; set; }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
