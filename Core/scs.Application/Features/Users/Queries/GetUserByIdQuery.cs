using MediatR;
using Scs.Application.DTOs;

namespace Scs.Application.Features.Users.Queries
{
    public record GetUserByIdQuery(int UserId) : IRequest<UserDto?>; 
}
