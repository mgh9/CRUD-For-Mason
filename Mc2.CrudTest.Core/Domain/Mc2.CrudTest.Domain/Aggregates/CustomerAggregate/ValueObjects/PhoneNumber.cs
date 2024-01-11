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
        public static PhoneNumber Create(string phoneNumber, IPhoneNumberValidator externalValidator)
        {
            //regionCode ??= "ZZ"; // if the business needs, we can refactor this in some other way
            Validate(phoneNumber, externalValidator);

            return new PhoneNumber(phoneNumber);
        }

        private static void Validate(string phoneNumber, IPhoneNumberValidator externalValidator)
        {
            if (!externalValidator.IsValid(phoneNumber, out string? message))
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
