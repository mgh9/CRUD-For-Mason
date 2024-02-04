namespace Mc2.CrudTest.Application.Customers.Models
{
    public sealed class UpdateCustomerDto
    {
        public Guid Id { get; set; }
        public string? NewFirstName { get; set; }
        public string NewLastName { get; set; }
        public string NewPhoneNumber { get; set; }
        public DateTime NewDateOfBirth { get; set; }
        public string NewEmail { get; set; }
    }
}
