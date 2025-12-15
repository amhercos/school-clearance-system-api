using MediatR;
using Microsoft.AspNetCore.Identity;
using Scs.Application.Exceptions;
using Scs.Application.Interfaces;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;
using Scs.Domain.Entities.Enums;

namespace Scs.Application.Features.Students.Commands
{
    public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand, Guid>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStudentRepository _studentRepository;

        public RegisterStudentCommandHandler(
            UserManager<ApplicationUser> userManager,
            IStudentRepository studentRepository)
        {
            _userManager = userManager;
            _studentRepository = studentRepository;
        }

        public async Task<Guid> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
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


                var studentProfile = new Student
                {
                    Id = user.Id,
                    DepartmentId = request.DepartmentId,
                    StudentNumber = request.StudentNumber,
                    YearLevel = request.YearLevel,
                    Course = request.Course
                };

                await _studentRepository.AddAsync(studentProfile, cancellationToken);
                await _studentRepository.SaveChangesAsync(cancellationToken);

                // Assign Role 
                var roleResult = await _userManager.AddToRoleAsync(user, "Student");
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
