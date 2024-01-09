using MediatR;

namespace Mc2.CrudTest.Domain.Abstractions.Events
{
    public abstract record DomainEvent() : INotification
    {
        public Guid EventId { get; } = Guid.NewGuid();
    }
}
