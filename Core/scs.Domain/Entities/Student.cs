using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scs.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentNumber { get; set; } // Unique school ID

        // Link to ASP.NET Identity User (Authentication)
        public string UserId { get; set; }

        // Academic Info
        public string Course { get; set; }
        public int YearLevel { get; set; }

        // Navigation Property
        public ClearanceForm ClearanceForm { get; set; }
    }
}
