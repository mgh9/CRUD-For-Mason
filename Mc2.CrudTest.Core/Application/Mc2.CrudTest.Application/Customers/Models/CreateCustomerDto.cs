namespace Mc2.CrudTest.Application.Customers.Models
{
    public sealed class CreateCustomerDto
    {
        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
