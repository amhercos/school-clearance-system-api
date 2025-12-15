namespace Scs.Application.DTOs
{
    public class StudentDetailsResponseDto
    {
        public string FullName { get; set; } 
        public string Email { get; set; }
        public string StudentNumber { get; set; }
        public string Course { get; set; }
        public string YearLevel { get; set; } // Map YearLevel enum to string
        public string DepartmentName { get; set; }
    }
}
