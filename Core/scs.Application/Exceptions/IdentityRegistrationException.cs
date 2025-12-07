using Microsoft.AspNetCore.Identity;

namespace Scs.Application.Exceptions;
public class IdentityRegistrationException : Exception
{
    public IdentityRegistrationException(IEnumerable<IdentityError> errors)
        : base("Identity registration failed.")
    {
        // Store errors for the API response
        Errors = errors.Select(e => e.Description).ToList();
    }
    public List<string> Errors { get; }
}