using MediatR;
using Microsoft.AspNetCore.Identity;
using Scs.Application.Interfaces;
using Scs.Domain.Entities;

namespace Scs.Application.Features.Auth.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return new LoginResult { Succeeded = false };
            }
            var roles = await _userManager.GetRolesAsync(user);
            string token = _jwtService.GenerateToken(user, roles);

            return new LoginResult
            {
                Succeeded = true,
                UserId = user.Id,
                Email = user.Email,
                Roles = roles,
                Token = token
            };
        }
    }
}