using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Interfaces.Repositories
{
    public interface IUserRepository :
        ICommandRepository<User>,
        IQueryRepository<User>
    {
        //Task<IEnumerable<User>> GetAllAsync();
        //Task<User> GetUserByIdAsync (int id);
        //Task AddUserAsync (User user);
        //Task UpdateUserAsync (User user);
        //Task DeleteUserAsync(int id);
    }
}
