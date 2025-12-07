using MediatR;
using Scs.Application.DTOs;
using Scs.Application.Interfaces;
using Scs.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.Departments.Queries
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IScsDbContext _dbContext;
        public GetDepartmentByIdQueryHandler(IDepartmentRepository repository, IScsDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }
        public async Task<DepartmentDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
           
            
            var department = await _repository.GetByIdAsync(request.Id);
            if (department == null)
            {
                return null;
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
