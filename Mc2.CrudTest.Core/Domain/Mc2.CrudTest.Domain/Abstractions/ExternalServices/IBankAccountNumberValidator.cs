namespace Mc2.CrudTest.Domain.Abstractions.ExternalServices
{
    public interface IBankAccountNumberValidator
    {
        bool IsValid(string bankAccountNumber, out string? message);
    }
}