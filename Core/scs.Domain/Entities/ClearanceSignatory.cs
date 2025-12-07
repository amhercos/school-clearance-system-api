using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Domain.Entities
{
    public class ClearanceSignatory : BaseEntity
    {
        public Guid DepartmentId { get; set; }
        public Guid FacultyId { get; set; }

    }
}
