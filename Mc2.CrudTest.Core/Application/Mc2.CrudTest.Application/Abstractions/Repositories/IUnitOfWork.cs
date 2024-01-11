using Mc2.CrudTest.Domain.Abstractions.Events;

namespace Mc2.CrudTest.Application.Abstractions.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task DispatchDomainEventsAsync(List<DomainEvent> domainEvents, CancellationToken cancellationToken = default);
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    }
}
