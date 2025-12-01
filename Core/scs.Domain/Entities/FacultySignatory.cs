using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scs.Domain.Entities
{
    public class FacultySignatory : User
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public string ClearanceRole { get; set; }

    }
}
