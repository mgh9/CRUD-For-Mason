using CSharpFunctionalExtensions;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects
{
    public class BankAccountNumber : ValueObject
    {
        public string Value { get; private set; }

        private BankAccountNumber(string value)
        {
            Value = value;
        }

        public static BankAccountNumber Create(string value)
        {
            Validate(value);

            return new BankAccountNumber(value);
        }

        private static void Validate(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
