using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Repositories.Base;
using System;

namespace Infra.Repositories
{
    public class RepositorySession : RepositoryBase<Session, Guid>, IRepositorySession
    {
        private readonly BaseContext _context;
        public RepositorySession(BaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
