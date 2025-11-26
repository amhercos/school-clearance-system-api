using System;
using System.Collections.Generic;
using System.Text;
using Scs.Domain.Entities;
using Scs.Domain.Interfaces;

namespace Scs.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return _dbContext.UserRepository.GetAllAsync();
        }

        public Task<User?> GetByIdAsync(int userId)
        {
            return _dbContext.UserRepository.GetByIdAsync(userId);
        }
    }
}
