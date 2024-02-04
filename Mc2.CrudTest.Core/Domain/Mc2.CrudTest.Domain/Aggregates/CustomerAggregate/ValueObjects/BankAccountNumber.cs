using CSharpFunctionalExtensions;
using Mc2.CrudTest.Domain.Abstractions.ExternalServices;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects
{
    public class BankAccountNumber : ValueObject
    {
        public string Value { get; private set; }

        private BankAccountNumber() 
        {
            // for de-hydration
        }

        private BankAccountNumber(string value)
        {
            Value = value;
        }

        public static BankAccountNumber Create(string bankAccountNumber, IBankAccountNumberValidator externalValidator)
        {
            Validate(bankAccountNumber, externalValidator);

            return new BankAccountNumber(bankAccountNumber);
        }

        private static void Validate(string bankAccountNumber, IBankAccountNumberValidator externalValidator)
        {
            if (!externalValidator.IsValid(bankAccountNumber, out string? message))
            {
                throw new InvalidBankAccountNumberException(bankAccountNumber, message);
            }
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
