using Mc2.CrudTest.Domain.Abstractions.DomainServices;
using Mc2.CrudTest.Domain.Abstractions.ExternalServices;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IPhoneNumberValidator _phoneNumberValidator;
        private readonly IBankAccountNumberValidator _bankAccountNumberValidator;

        public CustomerService(IPhoneNumberValidator phoneNumberValidator, IBankAccountNumberValidator bankAccountNumberValidator)
        {
            _phoneNumberValidator = phoneNumberValidator;
            _bankAccountNumberValidator = bankAccountNumberValidator;
        }

        public bool ValidateBankAccountNumber(string bankAccountNumber, out string? message)
        {
            return _bankAccountNumberValidator.IsValid(bankAccountNumber, out message);
        }

        public bool ValidatePhoneNumber(string phoneNumber, out string? message)
        {
            return _phoneNumberValidator.IsValid(phoneNumber, out message);
        }
    }
}