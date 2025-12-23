using Scs.Application.DTOs;
using Scs.Application.Interfaces.Repositories.Common;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Interfaces.Repositories
{
    public interface IFacultyRepository : 
        ICommandRepository<Faculty>, 
        IQueryRepository<Faculty>
    {
       
    }
}
