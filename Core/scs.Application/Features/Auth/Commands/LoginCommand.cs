using MediatR;
using System.ComponentModel.DataAnnotations;
using Scs.Application.DTOs;

namespace Scs.Application.Features.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResult>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}