using MediatR;
using Scs.Application.DTOs;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.Users.Commands
{
    public record CreateUserCommand(string FirstName, string LastName, string Email) : IRequest<UserDto>;
}
