using Mc2.CrudTest.Domain.Abstractions.Events;

namespace Mc2.CrudTest.Domain.Abstractions.Models
{
    public sealed class Event
    {
        public Event(DomainEvent domainEvent, long eventNumber, DateTime createdDate)
        {
            DomainEvent = domainEvent;
            EventNumber = eventNumber;
            CreatedDate = createdDate;
        }

        public long EventNumber { get; private set; }
        public DomainEvent DomainEvent { get; private set; }
        public DateTime CreatedDate { get; private set; }
    }
}
