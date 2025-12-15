using MediatR;
using Microsoft.AspNetCore.Identity;
using Scs.Application.Exceptions;
using Scs.Application.Interfaces;
using Scs.Domain.Entities;
using Scs.Domain.Entities.Enums;

namespace Scs.Application.Features.Students.Commands
{
    public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand, Guid>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IScsDbContext _context;

        public RegisterStudentCommandHandler(
            UserManager<ApplicationUser> userManager,
            IScsDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<Guid> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
        {
            // Create the ApplicationUser (Security Account)
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true // Or false, depending on your flow
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new IdentityRegistrationException(result.Errors);
            }

            // Create the Student Profile (Domain Entity) 
            var studentProfile = new Student
            {
                Id = user.Id,
                DepartmentId = request.DepartmentId,
                StudentNumber = request.StudentNumber,
                YearLevel = request.YearLevel,
                Course = request.Course
            };

            _context.Students.Add(studentProfile);
            await _context.SaveChangesAsync(cancellationToken);

            // Optional: Assign the 'Student' role
           var roleResult = await _userManager.AddToRoleAsync(user, "Student");
            if (!roleResult.Succeeded)
            {
                // Throw an exception with details if role assignment fails
                throw new IdentityRegistrationException(roleResult.Errors);
            }

            return user.Id;
        }
    }
}