using Mc2.CrudTest.Application.Abstractions.Repositories;
using Mc2.CrudTest.Domain.Abstractions.Models;

namespace Mc2.CrudTest.Infrastructure.Data.Repositories;

internal class Repository<T> : BaseRepository<T>, IRepository<T>
    where T : AggregateRoot
{
    public Repository(ApplicationDbContext context) 
        : base(context)
    {

    }
}
