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
            return new Email(value);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
