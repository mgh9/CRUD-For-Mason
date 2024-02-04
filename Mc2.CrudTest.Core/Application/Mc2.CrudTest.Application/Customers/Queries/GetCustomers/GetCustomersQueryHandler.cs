using Mc2.CrudTest.Application.Abstractions.Queries;
using Mc2.CrudTest.Application.Abstractions.Repositories;
using Mc2.CrudTest.Domain.Abstractions.Guards;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Application.Customers.Queries.GetCustomers
{
    public sealed class GetCustomersQueryHandler : QueryHandler<GetCustomersQuery, List<CustomerReadModel>>
    {
        private readonly IReadModelRepository<CustomerReadModel> _repository;

        public GetCustomersQueryHandler(IReadModelRepository<CustomerReadModel> repository)
            : base(null)
        {
            _repository = repository;
        }

        protected async override Task<List<CustomerReadModel>> HandleAsync(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _repository.GetAllAsync(cancellationToken: cancellationToken);

            return await customers.ToListAsync();
        }
    }
}
