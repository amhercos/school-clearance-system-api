using MediatR;
using Scs.Application.DTOs;
using Scs.Application.Features.Users.Commands;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // 1. Business logic (e.g., check for existing email, validation)

        // 2. Create Domain Entity (e.g., for simple user, use Student or Faculty based on logic)
        var newUser = new Student // Assuming all new users are students for now
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
        };

        // 3. Persistence
        //await _userRepository.AddFormAsync(newUser); // You'll need an Add method in IUserRepository
        //await _userRepository.SaveChangesAsync();

        return newUser;
    }

    Task<UserDto> IRequestHandler<CreateUserCommand, UserDto>.Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}