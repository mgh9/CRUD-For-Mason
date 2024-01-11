using CSharpFunctionalExtensions;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ValueObjects
{
    public class Email 
    {
        public string Value { get; }

        public Email(string value)
        {
            Value = value;
        }
    }
}
