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

        // the department responsible for signing
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        // Status sa specific signature
        public ClearanceFormStatus Status { get; set; }
        public string? Remarks { get; set; }         
        public DateTime? DateActioned { get; set; }

        // for the assigned signer
        public Guid? SignedByFacultyId { get; set; }
        public Faculty? SignedByFaculty { get; set; }
    }
}
