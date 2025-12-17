using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Scs.Application.Features.Faculties.Commands
{
    public class CreateFacultyCommand : IRequest<Guid>
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        public Guid? DepartmentId { get; set; }

    }
}
