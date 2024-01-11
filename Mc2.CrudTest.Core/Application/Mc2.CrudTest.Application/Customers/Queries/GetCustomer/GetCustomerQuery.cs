using Mc2.CrudTest.Application.Abstractions.Queries;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ReadModels;

namespace Mc2.CrudTest.Application.Customers.Queries.GetCustomer
{
    public sealed record GetCustomerQuery(Guid Id) : Query<CustomerReadModel>;
}
