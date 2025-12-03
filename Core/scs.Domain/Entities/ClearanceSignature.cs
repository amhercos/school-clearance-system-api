using Scs.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scs.Domain.Entities
{
    public class ClearanceSignature : BaseEntity
    {
        public Guid ClearanceFormId { get; set; }
        public ClearanceForm ClearanceForm { get; set; }

        // Which department needs to sign this?
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        // Status of this specific signature
        public ClearanceFormStatus Status { get; set; } // Pending, Approved, Rejected
        public string Remarks { get; set; }         
        public DateTime? DateActioned { get; set; }

        public Guid? SignedByFacultyId { get; set; }
        public Faculty SignedByFaculty { get; set; }
    }
}
