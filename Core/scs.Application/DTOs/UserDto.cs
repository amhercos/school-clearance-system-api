using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.DTOs
{
    public record UserDto(
    int UserId,
    string FirstName,
    string LastName,
    string? Email
    );
}
