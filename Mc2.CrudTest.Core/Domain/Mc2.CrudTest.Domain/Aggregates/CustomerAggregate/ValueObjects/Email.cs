using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects
{
    public class Email  :ValueObject
    {
        public string Value { get; private set; }

        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string value)
        {
            Validate(value);

            return new Email(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Email cannot be empty or null", nameof(value));
            }

            if(!IsValidEmailFormat(value))
            {
                throw new ArgumentException("Invalid email format", nameof(value));
            }
        }

        private static bool IsValidEmailFormat(string value)
        {
            string emailRegexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(value, emailRegexPattern);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
