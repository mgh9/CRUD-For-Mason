using Mc2.CrudTest.Domain.Abstractions.Events;

namespace Mc2.CrudTest.Domain.Abstractions.Models;

public abstract class AggregateRoot : BaseEntity
{
    private readonly List<DomainEvent> _domainEvents = [];
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected AggregateRoot()
        : this(Guid.NewGuid())
    {

    }

    protected AggregateRoot(Guid id)
    {
        Id = id;
    }

    protected virtual void AddDomainEvent(DomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    //public void LoadFromHistory(IEnumerable<DomainEvent> events)
    //{
    //    foreach (var @event in events)
    //    {
    //        When(@event);
    //    }
    //}

    //protected void ApplyChange(INotification @event)
    //{
    //    When(@event);
    //    _domainEvents.Add(@event);
    //}

    //protected abstract void When(INotification @event);
}