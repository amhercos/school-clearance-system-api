using MediatR;
using Scs.Application.DTOs;
using Scs.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.ClearanceForms.Queries
{
    public class GetClearanceFormByIdQueryHandler : IRequestHandler<GetClearanceFormByIdQuery, ClearanceFormDto>
    {
        private readonly IClearanceFormRepository _repository;
        public GetClearanceFormByIdQueryHandler(IClearanceFormRepository repository)
        {
            _repository = repository;
        }
        public async Task<ClearanceFormDto> Handle(GetClearanceFormByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                return null;
            }

            var clearanceFormDto = new ClearanceFormDto
            {
                Id = entity.Id,
                StudentId = entity.StudentId,
                OverallStatus = entity.OverallStatus.ToString(),
                IsCompleted = entity.IsCompleted,

                Signatures = entity.ClearanceSignatures.Select(s =>
                {
                    var faculty = s.SignedByFaculty;
                    var applicationUser = faculty?.ApplicationUser;

                    string signedByName;

                    if (applicationUser != null)
                    {
                        signedByName = $"{applicationUser.FirstName} {applicationUser.LastName}".Trim();
                    }
                    else if (faculty != null)
                    {
                        signedByName = faculty.EmployeeId;
                    }
                    else
                    {
                        signedByName = "N/A";
                    }

                    return new ClearanceSignatureDto
                    {
                        Id = s.Id,
                        DepartmentName = s.Department?.Name ?? "N/A",
                        Status = s.Status.ToString(),
                        Remarks = s.Remarks,
                        ClearanceFormId = s.ClearanceFormId,
                        IsSigned = s.DateActioned.HasValue,
                        SignedByFacultyName = signedByName,
                        SignedDate = s.DateActioned,
                    };
                }).ToList()
            };

            return clearanceFormDto;
        }
    }
}
        