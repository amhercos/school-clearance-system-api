using MediatR;
using Scs.Application.DTOs;
using Scs.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.Departments.Queries
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IReadOnlyList<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repository;
        public GetAllDepartmentsQueryHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<IReadOnlyList<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
           var entities = await _repository.GetAllAsync(cancellationToken);
            var resultDtos = entities
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    DepartmentCode = d.DepartmentCode
                })
                .ToList();

            return resultDtos;
        }
    }
}
