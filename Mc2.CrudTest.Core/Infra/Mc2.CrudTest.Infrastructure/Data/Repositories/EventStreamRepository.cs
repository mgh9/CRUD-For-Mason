using System.Collections.Concurrent;
using System.Reflection;
using Mc2.CrudTest.Application.Abstractions.Repositories;
using Mc2.CrudTest.Domain.Abstractions.Events;
using Mc2.CrudTest.Domain.Abstractions.Models;
using Mc2.CrudTest.Infrastructure.Data.Repositories.EventStore;

namespace Mc2.CrudTest.Infrastructure.Data.Repositories;

public sealed class EventStreamRepository<TAggregate> : IEventStreamRepository<TAggregate>
    where TAggregate : EventSourcedAggregate
{
    private readonly IEventStore _eventStore;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ConcurrentDictionary<Guid, TAggregate> _trackedAggregates = new();
    private static readonly ConstructorInfo _rehydrationFactory;

    static EventStreamRepository()
    {
        _rehydrationFactory = typeof(TAggregate)
                                .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                                    null, [], [])!;
    }

    public EventStreamRepository(IEventStore eventStore,
        IUnitOfWork unitOfWork)
    {
        _eventStore = eventStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (!_trackedAggregates.TryGetValue(id, out var aggregate))
        {
            aggregate = CreateEmptyAggregate();

            foreach (var @event in await _eventStore.ReadEventsAsync(id, cancellationToken))
            {
                aggregate.ApplyEvent(@event.DomainEvent);
            }

            _trackedAggregates.TryAdd(id, aggregate);
        }
        return aggregate;
    }

    public async Task<List<Event>> GetEventsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var events = await _eventStore.ReadEventsAsync(id, cancellationToken);
        return events.ToList();
    }

    public async Task SaveAsync(TAggregate aggregate, CancellationToken cancellationToken = default)
    {
        if (!_trackedAggregates.ContainsKey(aggregate.Id))
        {
            _trackedAggregates.TryAdd(aggregate.Id, aggregate);
        }

        await ProcessDomainEventsAsync(aggregate, cancellationToken);
    }

    private async Task ProcessDomainEventsAsync(TAggregate aggregate, CancellationToken cancellationToken)
    {
        var processedDomainEvents = new List<DomainEvent>();
        var unprocessedDomainEvents = aggregate.DomainEvents.ToList();

        while (unprocessedDomainEvents.Count != 0)
        {
            await _eventStore.AppendEventsAsync(aggregate.Id, aggregate.GetType().Name, unprocessedDomainEvents, cancellationToken);
            await _unitOfWork.DispatchDomainEventsAsync(unprocessedDomainEvents, cancellationToken);

            processedDomainEvents.AddRange(unprocessedDomainEvents);

            unprocessedDomainEvents = aggregate.DomainEvents
                                        .Where(e => !processedDomainEvents.Contains(e))
                                        .ToList();
        }

        aggregate.ClearDomainEvents();
    }

    private TAggregate CreateEmptyAggregate()
    {
        return (TAggregate)_rehydrationFactory.Invoke([]);
    }
}
