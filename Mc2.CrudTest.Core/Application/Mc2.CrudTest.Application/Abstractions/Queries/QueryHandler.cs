using AutoMapper;
using MediatR;

namespace Mc2.CrudTest.Application.Abstractions.Queries
{
    public abstract class QueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : Query<TResponse>
    {
        protected readonly IMapper? Mapper;

        public QueryHandler(IMapper? mapper)
        {
            Mapper = mapper;
        }

        public async Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken = default)
        {
            return await HandleAsync(request, cancellationToken);
        }

        protected abstract Task<TResponse> HandleAsync(TQuery request, CancellationToken cancellationToken);
    }
}
