using MediatR;
using Scs.Application.DTOs;
using Scs.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.Students.Queries
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IReadOnlyList<StudentDetailsResponseDto>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetAllStudentsQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<IReadOnlyList<StudentDetailsResponseDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _studentRepository.GetMappedAsync(
                selector: s => new StudentDetailsResponseDto
                {
                    FullName = s.ApplicationUser != null ? $"{s.ApplicationUser.FirstName} {s.ApplicationUser.LastName}" : string.Empty,
                    Email = s.ApplicationUser != null ? s.ApplicationUser.Email : string.Empty,
                    StudentNumber = s.StudentNumber,
                    Course = s.Course,
                    YearLevel = s.YearLevel.ToString(),
                    DepartmentName = s.Department != null ? s.Department.Name : string.Empty
                },
                predicate: null,
                cancellationToken: cancellationToken);
        }
    }
 }

