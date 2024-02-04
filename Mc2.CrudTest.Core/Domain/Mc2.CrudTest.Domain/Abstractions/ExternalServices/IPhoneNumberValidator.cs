namespace Mc2.CrudTest.Domain.Abstractions.ExternalServices
{
    public interface IPhoneNumberValidator
    {
        bool IsValid(string phoneNumber, out string? message);
        bool IsValid(string? phoneNumber, string? regionCode, out string? message);
    }
}