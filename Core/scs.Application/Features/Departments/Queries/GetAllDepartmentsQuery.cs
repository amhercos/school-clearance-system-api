using MediatR;
using Scs.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.Departments.Queries
{
    public class GetAllDepartmentsQuery :IRequest<IReadOnlyList<DepartmentDto>>
    { 
    }
}
