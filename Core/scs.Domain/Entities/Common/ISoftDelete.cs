using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Domain.Entities.Common
{
    public interface ISoftDelete
    {
        bool IsDeleted{ get; set; }
        DateTime? DeletedOnUtc { get; set; }
    }
}
