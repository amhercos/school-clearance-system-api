using Microsoft.EntityFrameworkCore;
using Scs.Application.DTOs;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;
using Scs.Infrastructure.Persistence;
using Scs.Infrastructure.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Infrastructure.Repositories
{
    public class FacultyRepository : Repository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(ScsDbContext dbContext) : base(dbContext)
        {
        }

    }
}
