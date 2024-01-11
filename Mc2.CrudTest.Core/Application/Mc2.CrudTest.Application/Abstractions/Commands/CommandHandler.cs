using Mc2.CrudTest.Application.Abstractions.Repositories;

namespace Mc2.CrudTest.Application.Abstractions.Commands
{
    public abstract class CommandHandler
    {
        protected readonly IUnitOfWork UnitOfWork;
        
        protected CommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
