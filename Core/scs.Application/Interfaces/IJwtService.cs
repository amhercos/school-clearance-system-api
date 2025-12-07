using Scs.Domain.Entities;

namespace Scs.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
    }
}