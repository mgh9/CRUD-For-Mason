using Mc2.CrudTest.Domain.Abstractions.Exceptions;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions
{
    public class InvalidBankAccountNumberException: DomainException
    {
        public InvalidBankAccountNumberException(string bankAccountNumber)
            : base($"Invalid BankAccountNumber `{bankAccountNumber}`")
        {
            BankAccountNumber = bankAccountNumber;
        }

        public InvalidBankAccountNumberException(string bankAccountNumber, string message)
            : base($"Invalid BankAccountNumber `{bankAccountNumber}`. {message}")
        {
            BankAccountNumber = bankAccountNumber;
        }

        public InvalidBankAccountNumberException(string bankAccountNumber, string message, Exception inner)
            : base(message, inner)
        {
            BankAccountNumber = bankAccountNumber;
        }

        public string BankAccountNumber { get; }
    }
}
