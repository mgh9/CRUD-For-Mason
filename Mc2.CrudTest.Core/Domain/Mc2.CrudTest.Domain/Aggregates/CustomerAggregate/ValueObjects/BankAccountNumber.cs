using CSharpFunctionalExtensions;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects
{
    public class BankAccountNumber 
    {
        public string Value { get; }

        public BankAccountNumber(string value)
        {
            Value = value;
        }
    }
}
