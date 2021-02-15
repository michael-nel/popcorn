using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Repositories.Base;
using System;

namespace Infra.Repositories
{
    public class RepositoryUser: RepositoryBase<User, Guid>, IRepositoryUser
    {
        private readonly BaseContext _context;
        public RepositoryUser(BaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
