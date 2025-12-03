using MediatR;
using Scs.Application.DTOs;
using Scs.Application.Features.Users.Queries;
using Scs.Application.Interfaces.Repositories;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.UserId);

      
        return user != null
            ? new UserDto(user.UserId, user.FirstName, user.LastName, user.Email)
            : null;
    }
}