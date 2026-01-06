using MediatR;
using Microsoft.AspNetCore.Identity;
using Scs.Application.Exceptions;
using Scs.Application.Interfaces.Repositories;
using Scs.Application.Interfaces.Services;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.Faculties.Commands
{
    public class CreateFacultyCommandHandler : IRequestHandler<CreateFacultyCommand, Guid>
    {
        private readonly IIdentityService _identityService;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public CreateFacultyCommandHandler(
            IIdentityService identityService,
            IFacultyRepository facultyRepository
            , IDepartmentRepository departmentRepository)
        {
            _identityService = identityService;
            _facultyRepository = facultyRepository;
            _departmentRepository = departmentRepository;
        }
        public async Task<Guid> Handle(CreateFacultyCommand request, CancellationToken cancellationToken)
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
               "Faculty", 
               cancellationToken);

            try
            {
                var faculty = new Faculty
                {
                    Id = userId,
                    EmployeeId = request.EmployeeId,
                    DepartmentId = request.DepartmentId
                };
                await _facultyRepository.AddAsync(faculty, cancellationToken);
                await _facultyRepository.SaveChangesAsync(cancellationToken);
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
