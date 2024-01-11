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
            return new PhoneNumber(value);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
