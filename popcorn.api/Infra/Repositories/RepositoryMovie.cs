using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Repositories.Base;
using System;

namespace Infra.Repositories
{
    public class RepositoryMovie : RepositoryBase<Movie, Guid>, IRepositoryMovie
    {
        private readonly BaseContext _context;
        public RepositoryMovie(BaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
