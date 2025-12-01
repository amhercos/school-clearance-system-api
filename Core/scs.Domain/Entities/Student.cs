using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scs.Domain.Entities
{
    public class Student : User
    {
        public string? StudentNumber { get; set; }
        public string? Course { get; set; }
        public string? YearLevel { get; set; }
        public int ? DepartmentId { get; set; }
        public Department? Department { get; set; }


        public virtual ICollection<ClearanceForm> ClearanceForms { get; set; } = new HashSet<ClearanceForm>();
    }
}
