using Microsoft.AspNetCore.Identity;

namespace Scs.Application.Exceptions;
public class IdentityRegistrationException : Exception
{
    public IdentityRegistrationException(IEnumerable<IdentityError> errors)
          : base($"Identity registration failed: {string.Join(", ", errors.Select(e => e.Description))}")
    {
        Errors = errors.Select(e => e.Description).ToList();
    }
    public List<string> Errors { get; }
}