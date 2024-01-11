using Mc2.CrudTest.Application.Abstractions.Repositories;
using Mc2.CrudTest.Domain.Abstractions.Models;

namespace Mc2.CrudTest.Infrastructure.Data.Repositories;

internal class ReadModelRepository<T> : BaseRepository<T>, IReadModelRepository<T>
    where T : BaseReadModel
{
    public ReadModelRepository(ApplicationDbContext context) : base(context)
    {

    }
}
