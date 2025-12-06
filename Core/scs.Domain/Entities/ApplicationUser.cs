using Microsoft.AspNetCore.Identity;

namespace Scs.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        // navigation propeties
        public Student StudentProfile { get; set; }
        public Faculty FacultyProfile { get; set; }

        // others
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // readonly wla sa Db
        public string FullName => $"{FirstName} {LastName}";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
