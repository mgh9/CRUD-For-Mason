using Mc2.CrudTest.Domain.Abstractions.Events;
using Mc2.CrudTest.Domain.Abstractions.Models;

namespace Mc2.CrudTest.Infrastructure.Data.Repositories.EventStore;

public interface IEventStore
{
    Task<IEnumerable<Event>> ReadEventsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<AppendResult> AppendEventsAsync(Guid id, string aggregateType, List<DomainEvent> events, CancellationToken cancellationToken = default);
}
