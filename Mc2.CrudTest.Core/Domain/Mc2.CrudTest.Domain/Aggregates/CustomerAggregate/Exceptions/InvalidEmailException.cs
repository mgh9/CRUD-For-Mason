using Mc2.CrudTest.Domain.Abstractions.Exceptions;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions
{
    public class InvalidEmailException : DomainException
    {
        public InvalidEmailException(string email)
            : base($"Invalid Email `{email}`")
        {
            Email = email;
        }

        public InvalidEmailException(string email, string message)
            : base($"Invalid Email `{email}`. {message}")
        {
            Email = email;
        }

        public InvalidEmailException(string email, string message, Exception inner)
            : base(message, inner)
        {
            Email = email;
        }

        public string Email { get; }
    }
}