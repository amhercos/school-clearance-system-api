using Microsoft.EntityFrameworkCore;
using Scs.Application.DTOs;
using Scs.Application.Exceptions;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;
using Scs.Infrastructure.Persistence;
using Scs.Infrastructure.Repositories.Common;


namespace Scs.Infrastructure.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ScsDbContext dbContext) : base(dbContext)
        {
        }
   

        public async Task<Student?> GetStudentDetailsAsync(Guid studentId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
             .AsNoTracking()
             .Include(s => s.Department)
             .Include(s => s.ApplicationUser)
             .SingleOrDefaultAsync(s => s.Id == studentId, cancellationToken);
        }


    }
}
