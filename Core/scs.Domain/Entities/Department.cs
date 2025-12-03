using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } // e.g., "Library", "Cashier", "Office of Student Affairs"
        public string Description { get; set; }

        public ICollection<Faculty> Faculties { get; set; }
    }
}
