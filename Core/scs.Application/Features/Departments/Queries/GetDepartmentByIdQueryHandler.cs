using MediatR;
using Scs.Application.DTOs;
using Scs.Application.Exceptions;
using Scs.Application.Interfaces;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.Departments.Queries
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>
    {
        private readonly IDepartmentRepository _repository;
        public GetDepartmentByIdQueryHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<DepartmentDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
           
            
            var department = await _repository.GetByIdAsync(request.Id);
            if (department == null)
            {
                throw new NotFoundException(nameof(Department), request.Id);
            }
            var departmentDto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                DepartmentCode = department.DepartmentCode
            };
            return departmentDto;
        }
    }
}
