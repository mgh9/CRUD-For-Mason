using MediatR;

namespace Mc2.CrudTest.Application.Abstractions.Queries
{
    public abstract record Query<T> : IRequest<T>;
}
