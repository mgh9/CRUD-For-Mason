using Mc2.CrudTest.Domain.Abstractions.Exceptions;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Exceptions
{
    public class InvalidInputDataException : DomainException
    {
        public InvalidInputDataException(object inputData)
            : base($"Invalid input data")
        {
            InputData = inputData;
        }

        public InvalidInputDataException(string message) : base(message) { }
        public InvalidInputDataException(string message, Exception inner) : base(message, inner) { }

        public object InputData { get; }
    }
}
