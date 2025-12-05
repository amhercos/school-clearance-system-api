using MediatR;

namespace Scs.Application.Features.ClearanceForms.Commands
{
    public class CreateClearanceFormCommand : IRequest<Guid>
    {
        public Guid StudentId { get; set; }
        public string AcademicYear { get; set; }
        public string Semester { get; set; }
        public string Program { get; set; }
        public string YearLevel { get; set; }
    }
}
