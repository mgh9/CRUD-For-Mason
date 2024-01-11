using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PhoneNumbers;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private PhoneNumber(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static PhoneNumber Create(string phoneNumber, string? region = "INT" )
        {
            Validate(phoneNumber, region);

            return new PhoneNumber(phoneNumber);
        }

        private static void Validate(string value, string? region)
        {
            //if (string.IsNullOrWhiteSpace(value))
            //{
            //    throw new ArgumentException("Phone number cannot be null or empty", nameof(value));
            //}

            //if (!IsValidPhoneNumberFormat(value))
            //{
            //    throw new ArgumentException($"Invalid phone number format: {value}", nameof(value));
            //}

            try
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                var phoneNumber = phoneNumberUtil.Parse(value, region);

                if (phoneNumberUtil.IsValidNumber(phoneNumber))
                {
                    throw new ArgumentException($"Invalid phone number format: {value}", nameof(value));
                }
            }
            catch (NumberParseException)
            {
                throw new ArgumentException($"Invalid phone number format: {value}", nameof(value));
            }
            catch (Exception)
            {
                throw new ArgumentException($"Invalid phone number: {value}", nameof(value));
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
