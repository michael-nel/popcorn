using Infra.Repositories.Base;

namespace Infra.Repositories.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseContext _context;

        public UnitOfWork(BaseContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
