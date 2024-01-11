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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="regionCode">ZZ means internationl region code</param>
        /// <returns></returns>
        public static PhoneNumber Create(string phoneNumber, string regionCode = "ZZ")
        {
            Validate(phoneNumber, regionCode);

            return new PhoneNumber(phoneNumber);
        }

        private static void Validate(string phoneNumber, string regionCode)
        {
            try
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                var parsedPhoneNumber = phoneNumberUtil.Parse(phoneNumber, regionCode);

                if (!phoneNumberUtil.IsValidNumber(parsedPhoneNumber))
                {
                    throw new ArgumentException($"Invalid phone number format: {phoneNumber}", nameof(phoneNumber));
                }
            }
            catch (NumberParseException npx)
            {
                throw new ArgumentException($"Invalid phone number format: {phoneNumber}", nameof(phoneNumber));
            }
            catch (Exception)
            {
                throw new ArgumentException($"Invalid phone number: {phoneNumber}", nameof(phoneNumber));
            }
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
