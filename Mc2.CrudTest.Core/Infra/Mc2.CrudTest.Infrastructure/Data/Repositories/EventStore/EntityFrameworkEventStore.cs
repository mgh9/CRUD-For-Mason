using Mc2.CrudTest.Application.Abstractions.Repositories;
using Mc2.CrudTest.Domain.Abstractions.Events;
using Mc2.CrudTest.Domain.Abstractions.Exceptions;
using Mc2.CrudTest.Domain.Abstractions.Models;

namespace Mc2.CrudTest.Infrastructure.Data.Repositories.EventStore;

public sealed class EntityFrameworkEventStore : IEventStore
{
    private readonly IRepository<EventStream> _repository;

    public EntityFrameworkEventStore(IRepository<EventStream> repository)
    {
        _repository = repository;
    }

    public async Task<AppendResult> AppendEventsAsync(Guid id, string aggregateType, List<DomainEvent> events, CancellationToken cancellationToken = default)
    {
        var eventStream = await GetEventStreamAsync(id, cancellationToken);

        if (eventStream == null)
        {
            eventStream = EventStream.Create(id, aggregateType);
            _repository.Add(eventStream);
        }

        foreach (var @event in events)
        {
            var streamEvent = new Event(@event, eventStream.Version + 1, DateTime.UtcNow);
            eventStream.AddEvent(streamEvent);
        }

        var result = new AppendResult(eventStream.Version);
        return result;
    }

    public async Task<IEnumerable<Event>> ReadEventsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var eventStream = await FindEventStreamAsync(id, cancellationToken);
        return eventStream.Events;
    }

    private async Task<EventStream> FindEventStreamAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var eventStream = await GetEventStreamAsync(id, cancellationToken);
        if (eventStream == null)
        {
            throw new NotFoundException();
        }

        return eventStream;
    }

    private async Task<EventStream?> GetEventStreamAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var eventStream = await _repository.GetByIdAsync(id, cancellationToken);
        return eventStream;
    }
}
