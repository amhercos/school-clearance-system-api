using Microsoft.AspNetCore.Identity;
using Scs.Application.Exceptions;
using Scs.Domain.Entities;

namespace Scs.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<Guid> CreateUserAsync(string email, string firstName, string lastName, string password, string role, CancellationToken cancellationToken = default);
        Task DeleteUserAsync(Guid userId);
        Task<bool> UserExistsAsync(string email);

    }
}
