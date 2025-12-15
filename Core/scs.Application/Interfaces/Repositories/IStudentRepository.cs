using Scs.Application.DTOs;
using Scs.Application.Interfaces.Repositories.Common;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Interfaces.Repositories
{
    public interface IStudentRepository : 
        ICommandRepository<Student>, 
        IQueryRepository<Student>
    {
        Task<Student?> GetStudentDetailsAsync(Guid studentId, CancellationToken cancellationToken = default);
    }
}
