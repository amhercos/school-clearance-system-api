using Scs.Domain.Entities.Enums;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scs.Domain.Entities
{
    public class ClearanceForm
    {
        public int FormId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateOnly DateRqeuested { get; set; }
        public ClearanceFormStatus Status { get; set; }
        public virtual ICollection<ClearanceSignature> Signatures { get; set; } = new HashSet<ClearanceSignature>();
    }
}
