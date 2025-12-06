using Scs.Domain.Entities.Enums;
namespace Scs.Domain.Entities

{
    public class Student : BaseEntity
    {
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string StudentNumber { get; set; }
        public string? Course { get; set; }
        public YearLevel YearLevel { get; set; }
       public ICollection<ClearanceForm> ClearanceForms { get; set; } = new HashSet<ClearanceForm>();
    }
}
