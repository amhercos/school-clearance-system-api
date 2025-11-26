using Scs.Application.DTOs;
using Scs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            // Map Domain Entities to DTOs using LINQ
            return users.Select(u => new UserDto(u.UserId, u.FirstName, u.LastName, u.Email));
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            // Map if user is found, otherwise return null
            return user != null
                ? new UserDto(user.UserId, user.FirstName, user.LastName, user.Email)
                : null;
        }
    }
}
