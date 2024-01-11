using CSharpFunctionalExtensions;
using Mc2.CrudTest.Domain.Abstractions.ExternalServices;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private PhoneNumber()
        {
            // for de-hyd
        }

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
        public static PhoneNumber Create(string phoneNumber, IPhoneNumberValidator externalValidator, string regionCode)
        {
            regionCode ??= "ZZ"; // if business needs, we can refactor this in some other way
            Validate(phoneNumber, regionCode, externalValidator);

            return new PhoneNumber(phoneNumber);
        }

        private static void Validate(string phoneNumber, string regionCode , IPhoneNumberValidator externalValidator)
        {
            if (!externalValidator.IsValid(phoneNumber, regionCode, out string? message))
            {
                throw new InvalidPhoneNumberException(phoneNumber, message);
            }
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
