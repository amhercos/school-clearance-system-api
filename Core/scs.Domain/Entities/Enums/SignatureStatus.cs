using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scs.Domain.Entities.Enums
{
    public enum SignatureStatus
    {
        Required = 0,
        Signed = 1,
        PendingObligation = 2,
        NotApplicable = 3
    }
}
