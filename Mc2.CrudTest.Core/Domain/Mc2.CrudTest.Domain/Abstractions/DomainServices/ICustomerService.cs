namespace Mc2.CrudTest.Domain.Abstractions.DomainServices
{
    public interface ICustomerService
    {
        bool ValidatePhoneNumber(string phoneNumber, out string? message);
        bool ValidateBankAccountNumber(string bankAccountNumber, out string? message);
    }
}
