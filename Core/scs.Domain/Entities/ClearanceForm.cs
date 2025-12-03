using Scs.Domain.Entities.Enums;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scs.Domain.Entities
{
    public class ClearanceForm : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public string AcademicYear { get; set; }
        public string Semester { get; set; }

        // Overall status of the whole form
        public ClearanceFormStatus OverallStatus { get; set; }
        public DateTime? DateCompleted { get; set; }

        public bool IsActive { get; set; }       
        public bool IsCompleted { get; set; }  

        // The list of signatures required
        public ICollection<ClearanceSignature> ClearanceSignatures { get; set; } = new HashSet<ClearanceSignature>();   

    }
}
