using MediatR;
using Scs.Application.DTOs;
using Scs.Application.Interfaces.Repositories;

namespace Scs.Application.Features.Faculties.Queries
{
    public class GetAllFacultiesQueryHandler : IRequestHandler<GetAllFacultiesQuery, IReadOnlyList<FacultyDto>>
    {
        private readonly IFacultyRepository _facultyRepository;

        public GetAllFacultiesQueryHandler(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }
        public async Task<IReadOnlyList<FacultyDto>> Handle(GetAllFacultiesQuery request, CancellationToken cancellationToken)
        {
            return await _facultyRepository.GetMappedAsync(
                selector: f => new FacultyDto
                {
                    Id = f.Id,
                    EmployeeId = f.EmployeeId,
                    FullName = f.ApplicationUser != null
                                ? f.ApplicationUser.FirstName + " " + f.ApplicationUser.LastName
                                : "No User Linked",
                    Email = f.ApplicationUser != null ? f.ApplicationUser.Email : "N/A",
                    DepartmentName = f.Department.Name ?? "Unassigned",
                    DepartmentId = f.DepartmentId
                },
               //filter which rows
                predicate: f => f.IsDeleted == false, 
                cancellationToken: cancellationToken
            );
        }
    }
}
