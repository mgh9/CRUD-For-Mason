using Mc2.CrudTest.Application.Abstractions.Queries;
using Mc2.CrudTest.Application.Abstractions.Repositories;
using Mc2.CrudTest.Domain.Abstractions.Guards;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ReadModels;

namespace Mc2.CrudTest.Application.Customers.Queries.GetCustomer
{
    public sealed class GetCustomerQueryHandler : QueryHandler<GetCustomerQuery, CustomerReadModel>
    {
        private readonly IReadModelRepository<CustomerReadModel> _repository;

        public GetCustomerQueryHandler(IReadModelRepository<CustomerReadModel> repository) 
            : base(null)
        {
            _repository = repository;
        }

        protected async override Task<CustomerReadModel> HandleAsync(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);

            return Guard.Against.NotFound(customer);
        }
    }
}
