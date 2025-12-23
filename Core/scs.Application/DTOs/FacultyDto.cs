using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.DTOs
{
    public class FacultyDto
    {
        public Guid Id { get; set; }
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? DepartmentName { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
