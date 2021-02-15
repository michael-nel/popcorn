using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Repositories.Base;
using System;

namespace Infra.Repositories
{
    public class RepositoryRoom : RepositoryBase<Room, Guid>, IRepositoryRoom
    {
        private readonly BaseContext _context;
        public RepositoryRoom(BaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
