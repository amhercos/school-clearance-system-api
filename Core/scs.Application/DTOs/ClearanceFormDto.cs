

namespace Scs.Application.DTOs
{
    public class ClearanceFormDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string AcademicYear { get; set; }
        public string Semester { get; set; }
        public string Program { get; set; }
        public string YearLevel { get; set; }
        public string OverallStatus { get; set; } // Use string representation of the Enum
        public bool IsCompleted { get; set; }

        // Include DTOs for nested collections
        public IReadOnlyList<ClearanceSignatureDto> Signatures { get; set; } = new List<ClearanceSignatureDto>();
    }
}
