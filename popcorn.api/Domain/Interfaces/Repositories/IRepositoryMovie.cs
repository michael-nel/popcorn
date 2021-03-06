﻿using Domain.Entities;
using Domain.Interfaces.Repositories.Base;
using System;

namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryMovie : IRepositoryBase<Movie, Guid>
    {
    }
}
