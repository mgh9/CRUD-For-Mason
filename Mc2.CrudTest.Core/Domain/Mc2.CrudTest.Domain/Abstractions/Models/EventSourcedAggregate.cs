using Mc2.CrudTest.Domain.Abstractions.Events;

namespace Mc2.CrudTest.Domain.Abstractions.Models;

public abstract class EventSourcedAggregate : AggregateRoot
{
    public void ApplyEvent(DomainEvent @event)
    {
        ((dynamic)this).Apply((dynamic)@event);
    }

    protected void AddEvent(DomainEvent @event)
    {
        ApplyEvent(@event);
        base.AddDomainEvent(@event);
    }

    protected override void AddDomainEvent(DomainEvent eventItem)
    {
        throw new NotSupportedException($"Please use {nameof(AddEvent)} instead.");
    }
}
