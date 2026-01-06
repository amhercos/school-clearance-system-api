using MediatR;
using Microsoft.AspNetCore.Identity;
using Scs.Application.Exceptions;
using Scs.Application.Interfaces;
using Scs.Application.Interfaces.Repositories;
using Scs.Application.Interfaces.Services;
using Scs.Domain.Entities;
using Scs.Domain.Entities.Enums;

namespace Scs.Application.Features.Students.Commands
{
    public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand, Guid>
    {
        private readonly IIdentityService _identityService;
        private readonly IStudentRepository _studentRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public RegisterStudentCommandHandler(
            IIdentityService identityService,
            IStudentRepository studentRepository,
            IDepartmentRepository departmentRepository)
        {
            _identityService = identityService;
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<Guid> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.DepartmentId, cancellationToken);
            if (department == null)
            {
                throw new NotFoundException(nameof(Department), request.DepartmentId);
            }

            var userId = await _identityService.CreateUserAsync(
               request.Email,
               request.FirstName,
               request.LastName,
               request.Password,
               "Student",
               cancellationToken);

            try
            {
                var student = new Student
                {
                    Id = userId,
                    StudentNumber = request.StudentNumber,
                    DepartmentId = request.DepartmentId,
                    YearLevel = request.YearLevel,
                    Course = request.Course
                };
                await _studentRepository.AddAsync(student, cancellationToken);
                await _studentRepository.SaveChangesAsync(cancellationToken);
                return userId;
            }

            catch
            {
                // Rollback user creation if faculty profile fails
                await _identityService.DeleteUserAsync(userId);
                throw;
            }
        }
    }

}    
