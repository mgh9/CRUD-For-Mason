using Mc2.CrudTest.Domain.Abstractions.Models;

namespace Mc2.CrudTest.Application.Abstractions.Repositories
{
    public interface IReadModelRepository<T> : IBaseRepository<T>
        where T : BaseReadModel
    {

    }

}
