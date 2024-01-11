using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects
{
    public class PhoneNumber: ValueObject
    {
        private PhoneNumber(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static PhoneNumber Create(string value)
        {
            Validate(value);

            return new PhoneNumber(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Phone number cannot be null or empty", nameof(value));
            }

            if (!IsValidPhoneNumberFormat(value))
            {
                throw new ArgumentException($"Invalid phone number format: {value}", nameof(value));
            }
        }

        private static bool IsValidPhoneNumberFormat(string value)
        {
            string phoneRegexPattern = @"^(\+|00)[1-9][0-9 \-\(\)\.]{7,32}$";
            return Regex.IsMatch(value, phoneRegexPattern);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
