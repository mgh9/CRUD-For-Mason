using Mc2.CrudTest.Application.Abstractions.Repositories;
using MediatR;

namespace Mc2.CrudTest.Application.Abstractions.Commands
{
    public abstract class CommandHandler<TCommand> : CommandHandler, IRequestHandler<TCommand>
        where TCommand : Command
    {
        protected CommandHandler(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public async Task Handle(TCommand request, CancellationToken cancellationToken = default)
        {
            await HandleAsync(request, cancellationToken);
        }

        protected abstract Task HandleAsync(TCommand request, CancellationToken cancellationToken);
    }
}
