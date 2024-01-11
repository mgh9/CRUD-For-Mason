using Mc2.CrudTest.Domain.Abstractions.Exceptions;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions
{
    public class InvalidPhoneNumberException : DomainException
    {
        public InvalidPhoneNumberException(string phoneNumber)
            : base($"Invalid PhoneNumber `{phoneNumber}`")
        {
            PhoneNumber = phoneNumber;
        }

        public InvalidPhoneNumberException(string phoneNumber, string message)
            : base($"Invalid PhoneNumber `{phoneNumber}`. {message}")
        {
            PhoneNumber = phoneNumber;
        }

        public InvalidPhoneNumberException(string phoneNumber, string message, Exception inner)
            : base(message, inner)
        {
            PhoneNumber = phoneNumber;
        }

        public string PhoneNumber { get; }
    }
}
