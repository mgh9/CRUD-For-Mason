using MediatR;

namespace Mc2.CrudTest.Application.Abstractions.Commands
{
    public abstract record Command : IRequest;
}
