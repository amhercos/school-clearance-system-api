using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Domain.Entities
{
    public class Faculty : BaseEntity
    {
        public ApplicationUser ApplicationUser { get; set; }
        public string EmployeeId { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
