using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Domain.Entities
{
    public class ClearanceRule : BaseEntity
    {
        public Guid RequiredDepartmentId { get; set; }
        public Guid? StudentDepartmentId { get; set; }
        public bool IsGeneralRequirement => StudentDepartmentId == null;
        public Department RequiredDepartment { get; set; }
        public Department? StudentDepartment { get; set; }

    }
}
