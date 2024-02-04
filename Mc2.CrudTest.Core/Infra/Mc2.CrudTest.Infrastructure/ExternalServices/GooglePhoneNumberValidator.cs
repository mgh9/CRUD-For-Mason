using Mc2.CrudTest.Domain.Abstractions.ExternalServices;
using PhoneNumbers;

namespace Mc2.CrudTest.Infrastructure.ExternalServices;

public class GooglePhoneNumberValidator : IPhoneNumberValidator
{
    private readonly PhoneNumbers.PhoneNumberUtil _phoneNumbersUtil;

    public GooglePhoneNumberValidator()
    {
        _phoneNumbersUtil = PhoneNumberUtil.GetInstance();
    }

    public bool IsValid(string? phoneNumber, out string? message)
    {
        return IsValid(phoneNumber, null, out message);
    }

    public bool IsValid(string? phoneNumber, string? regionCode, out string? message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);

        try
        {
            var parsedPhone = _phoneNumbersUtil.Parse(phoneNumber, regionCode);
            
            var result = _phoneNumbersUtil.IsValidNumber(parsedPhone);
            message = null;

            return result;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return false;
        }
    }
}
