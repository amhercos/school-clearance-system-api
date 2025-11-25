using Scs.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scs.Domain.Entities
{
    public class ClearanceSignature
    {
        public int SignatureId { get; set; }
        public int SignatoryId { get; set; }
        public SignatureStatus SignatureStatus { get; set; }
        public DateOnly DateSigned { get; set; }
    }
}
