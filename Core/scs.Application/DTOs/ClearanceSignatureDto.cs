namespace Scs.Application.DTOs
{
    public class ClearanceSignatureDto
    {
        public Guid Id { get; set; }
        public Guid ClearanceFormId { get; set; }

        public string DepartmentName { get; set; }
        public string Status { get; set; } 
        public string? Remarks { get; set; }

        public bool IsSigned { get; set; } 
        public string? SignedByFacultyName { get; set; } 
        public DateTime? SignedDate { get; set; }
    }
}
