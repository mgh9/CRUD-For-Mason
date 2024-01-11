using Mc2.CrudTest.Domain.Abstractions.Models;

namespace Mc2.CrudTest.Application.Abstractions.Repositories
{
    public interface IEventStreamRepository<TAggregate> 
        where TAggregate : EventSourcedAggregate
    {
        Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Event>> GetEventsAsync(Guid id, CancellationToken cancellationToken = default);
        Task SaveAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
    }
}
