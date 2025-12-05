using Scs.Domain.Entities.Enums;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Scs.Domain.Entities
{
    public class ClearanceForm : BaseEntity
    {
        private ClearanceForm()
        {
            ClearanceSignatures = new HashSet<ClearanceSignature>();
            OverallStatus = ClearanceFormStatus.Pending;
            IsActive = true;
            IsCompleted = false;
        }

       
        public static ClearanceForm Create(
            Guid studentId,
            string academicYear,
            string semester,
            string program,
            string yearLevel) 
        {
        var form = new ClearanceForm
            {
                StudentId = studentId,
                AcademicYear = academicYear,
                Semester = semester,
                Program = program,
                YearLevel = yearLevel,
            };

            return form;
        }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public string AcademicYear { get; set; }
        public string Semester { get; set; }
        public string Program { get; set; }
        public string YearLevel { get; set; }

        // Clearance Status
        public ClearanceFormStatus OverallStatus { get; set; }
        public DateTime? DateCompleted { get; set; }

        public bool IsActive { get; set; }
        public bool IsCompleted { get; set; }

   
        public ICollection<ClearanceSignature> ClearanceSignatures { get; set; } = new HashSet<ClearanceSignature>();

    }
}