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
          public async Task<IReadOnlyList<DepartmentDto>> Handle(GetAllDepartmentsQuery request, 
             
              CancellationToken cancellationToken)
        {
       

            return await _repository.GetMappedAsync(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                DepartmentCode = d.DepartmentCode
            }, 

                predicate : null
            , cancellationToken);
        }

    }
}

