using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.Departments.Commands
{
    public record DeleteDepartmentCommand(Guid Id) : IRequest;
    
}
