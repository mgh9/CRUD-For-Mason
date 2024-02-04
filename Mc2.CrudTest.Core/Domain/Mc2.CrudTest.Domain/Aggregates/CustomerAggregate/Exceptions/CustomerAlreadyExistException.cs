using Mc2.CrudTest.Domain.Abstractions.Exceptions;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions
{
    public class CustomerAlreadyExistException : DomainException
    {
        public CustomerAlreadyExistException(string email)
            : base($"Customer with email `{email}` already exist")
        {
            Email = email;
        }

        public CustomerAlreadyExistException(string email, string message)
            : base($"Customer with email `{email}` already exist. {message}")
        {
            Email = email;
        }

        public CustomerAlreadyExistException(string email, string message, Exception inner)
            : base(message, inner)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
