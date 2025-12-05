using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Interfaces.Repositories
{
    public interface IClearanceFormRepository : 
        ICommandRepository<ClearanceForm>, 
        IQueryRepository<ClearanceForm>
    {

    }
}
