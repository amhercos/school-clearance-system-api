using MediatR;
using Microsoft.EntityFrameworkCore;
using Scs.Application.DTOs;
using Scs.Application.Exceptions;
using Scs.Application.Interfaces;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;
using Scs.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.Students.Queries
{
    public class GetStudentDetailsQueryHandler : IRequestHandler<GetStudentDetailsQuery, StudentDetailsResponseDto>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentDetailsQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        }

        public async Task<StudentDetailsResponseDto> Handle(GetStudentDetailsQuery query, CancellationToken cancellationToken)
        {
            var result = await _studentRepository.GetStudentDetailsAsync(query.StudentId, cancellationToken);
            if (result == null) throw new NotFoundException(nameof(Student), query.StudentId);
            if (result.ApplicationUser == null)
                throw new InvalidOperationException($"Student profile {query.StudentId} exists but its linked ApplicationUser is null.");

            var resultDto = new StudentDetailsResponseDto
            {
                FullName = $"{result.ApplicationUser.FirstName} {result.ApplicationUser.LastName}",
                Email = result.ApplicationUser.Email,
                StudentNumber = result.StudentNumber,
                Course = result.Course ?? string.Empty,
                YearLevel = result.YearLevel.ToString(),
                DepartmentName = result.Department!.Name
            };

            return resultDto;
        }
    }
}
