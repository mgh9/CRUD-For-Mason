using IbanNet;
using Mc2.CrudTest.Domain.Abstractions.ExternalServices;

namespace Mc2.CrudTest.Infrastructure.ExternalServices;

public class BankAccountNumberValidator : IBankAccountNumberValidator
{
    //private const string VALID_BANK_ACCOUNT_NUMBER_REGEX = @"^\d{8,20}$";
    private readonly IIbanValidator _ibanValidator;

    public BankAccountNumberValidator(IIbanValidator ibanValidator)
    {
        this._ibanValidator = ibanValidator;
    }

    public bool IsValid(string bankAccountNumber, out string? message)
    {
        // e.g : NL91ABNA0417164300

        //ArgumentException.ThrowIfNullOrWhiteSpace(bankAccountNumber);

        //if (!Regex.IsMatch(bankAccountNumber, VALID_BANK_ACCOUNT_NUMBER_REGEX))
        //{
        //    return false;
        //}

        var result = _ibanValidator.Validate(bankAccountNumber);
        message = result.Error?.ErrorMessage;

        return result.IsValid;
    }
}
