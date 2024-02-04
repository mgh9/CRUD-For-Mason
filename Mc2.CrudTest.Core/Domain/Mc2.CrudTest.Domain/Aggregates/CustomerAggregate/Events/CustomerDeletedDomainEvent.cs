using Mc2.CrudTest.Domain.Abstractions.Events;

namespace Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Events;

public sealed record CustomerDeletedDomainEvent(Guid Id, DateTime DeletedTime) : DomainEvent
{

}
