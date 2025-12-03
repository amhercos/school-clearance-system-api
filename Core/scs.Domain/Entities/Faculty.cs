using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Domain.Entities
{
    public class Faculty : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }

        // Link to ASP.NET Identity User
        public string UserId { get; set; }

        // Foreign Key
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
