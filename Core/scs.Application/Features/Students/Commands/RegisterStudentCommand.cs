using MediatR;
using Scs.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Scs.Application.Features.Students.Commands
{
    public class RegisterStudentCommand : IRequest<Guid>
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string StudentNumber { get; set; }

        public YearLevel YearLevel { get; set; }
        public string Course { get; set; }
    }
}