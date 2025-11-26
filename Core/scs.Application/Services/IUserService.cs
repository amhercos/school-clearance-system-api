using System;
using System.Collections.Generic;
using System.Text;
using Scs.Application.DTOs;

namespace Scs.Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int userId);
    }
}
