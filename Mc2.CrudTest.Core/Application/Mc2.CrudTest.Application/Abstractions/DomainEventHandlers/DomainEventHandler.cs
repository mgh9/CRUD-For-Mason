using Mc2.CrudTest.Domain.Abstractions.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Application.Abstractions.DomainEventHandlers
{
    public abstract class DomainEventHandler<T> : INotificationHandler<T>
        where T : DomainEvent
    {
        protected readonly ILogger<DomainEventHandler<T>> Logger;

        protected DomainEventHandler(ILogger<DomainEventHandler<T>> logger)
        {
            Logger = logger;
        }

        public async Task Handle(T notification, CancellationToken cancellationToken = default)
        {
            Logger.LogInformation("Processing a domain event: {type}", this.GetType().Name);
            await OnHandleAsync(notification);
            Logger.LogInformation("Completed processing a domain event: {type}", this.GetType().Name);
        }

        protected abstract Task OnHandleAsync(T @event);
    }
}
