using CSharpFunctionalExtensions;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects
{
    public class PhoneNumber 
    {
        public string Value { get; }

        public PhoneNumber(string value)
        {
            Value = value;
        }
    }
}
