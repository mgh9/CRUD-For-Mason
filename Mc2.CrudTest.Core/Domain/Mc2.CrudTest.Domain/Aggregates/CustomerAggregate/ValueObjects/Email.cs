using System.Net.Mail;
using CSharpFunctionalExtensions;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects
{
    public class Email  :ValueObject
    {
        private Email()
        {
            // for de-hydration
        }

        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string value)
        {
            Validate(value);

            return new Email(value);
        }

        public string Value { get; private set; }

        private static void Validate(string? email)
        {
            try
            {
                _ = new MailAddress(email);
            }
            catch
            {
                throw new InvalidEmailException(email);
            }
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
