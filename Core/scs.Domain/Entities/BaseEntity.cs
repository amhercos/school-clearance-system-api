using Scs.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Domain.Entities
{
    public abstract class BaseEntity : ISoftDelete
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; } // Soft delete
        public DateTime? DeletedOnUtc { get; set; }
    }
}
