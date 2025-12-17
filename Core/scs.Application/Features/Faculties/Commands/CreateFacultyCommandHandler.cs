using MediatR;
using Microsoft.AspNetCore.Identity;
using Scs.Application.Exceptions;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.Faculties.Commands
{
    public class CreateFacultyCommandHandler : IRequestHandler<CreateFacultyCommand, Guid>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFacultyRepository _facultyRepository;

        public CreateFacultyCommandHandler(UserManager<ApplicationUser> userManager, IFacultyRepository facultyRepository)
        {
            _userManager = userManager;
            _facultyRepository = facultyRepository;
        }
        public async Task<Guid> Handle(CreateFacultyCommand request, CancellationToken cancellationToken)
        {

            ApplicationUser user = null;
            try
            {
                user = new ApplicationUser

                {
                    UserName = request.Email,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    throw new IdentityRegistrationException(result.Errors);
                }

                var facultyProfile = new Faculty
                {
                    Id = user.Id,
                    EmployeeId = request.EmployeeId,
                    DepartmentId = request.DepartmentId
                };

                await _facultyRepository.AddAsync(facultyProfile, cancellationToken);
                await _facultyRepository.SaveChangesAsync(cancellationToken);

                // Assign Role 
                var roleResult = await _userManager.AddToRoleAsync(user, "Faculty");
                if (!roleResult.Succeeded)
                {
                    throw new IdentityRegistrationException(roleResult.Errors);
                }

                return user.Id;
                }
                catch (IdentityRegistrationException)
                {
                if (user != null && user.Id != Guid.Empty)
                {
                    await _userManager.DeleteAsync(user);
                }
                throw;
            }
        }
    }
}
