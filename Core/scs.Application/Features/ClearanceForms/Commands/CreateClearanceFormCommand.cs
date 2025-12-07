using MediatR;
using System.Text.Json.Serialization;

namespace Scs.Application.Features.ClearanceForms.Commands
{
    public class CreateClearanceFormCommand : IRequest<Guid>
    {
        [JsonIgnore]
        public Guid DerivedStudentId { get; set; }
        public string AcademicYear { get; set; }
        public string Semester { get; set; }
        public string Program { get; set; }
        public string YearLevel { get; set; }
    }
}
