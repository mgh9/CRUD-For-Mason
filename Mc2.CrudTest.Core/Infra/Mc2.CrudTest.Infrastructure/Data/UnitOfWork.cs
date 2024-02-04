using Mc2.CrudTest.Application.Abstractions.Repositories;
using Mc2.CrudTest.Domain.Abstractions.Events;
using Mc2.CrudTest.Domain.Abstractions.Models;
using MediatR;

namespace Mc2.CrudTest.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IPublisher _publisher;

        public UnitOfWork(ApplicationDbContext context, IPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }
        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            await DispatchEventsAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        private async Task DispatchEventsAsync(CancellationToken cancellationToken = default)
        {
            var processedDomainEvents = new List<DomainEvent>();
            var unprocessedDomainEvents = GetDomainEvents();

            // this is needed incase another DomainEvent is published from a DomainEventHandler
            while (unprocessedDomainEvents.Count != 0)
            {
                await DispatchDomainEventsAsync(unprocessedDomainEvents, cancellationToken);

                processedDomainEvents.AddRange(unprocessedDomainEvents);

                unprocessedDomainEvents = GetDomainEvents()
                                                .Where(e => !processedDomainEvents.Contains(e))
                                                .ToList();
            }

            ClearDomainEvents();
        }

        private List<DomainEvent> GetDomainEvents()
        {
            var aggregateRoots = GetTrackedAggregateRoots();

            return aggregateRoots
                .SelectMany(x => x.DomainEvents)
                .ToList();
        }

        private List<AggregateRoot> GetTrackedAggregateRoots()
        {
            return _context.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Count != 0)
                .Select(e => e.Entity)
                .ToList();
        }

        public async Task DispatchDomainEventsAsync(List<DomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }
        }

        private void ClearDomainEvents()
        {
            var aggregateRoots = GetTrackedAggregateRoots();
            aggregateRoots.ForEach(aggregate => aggregate.ClearDomainEvents());
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
