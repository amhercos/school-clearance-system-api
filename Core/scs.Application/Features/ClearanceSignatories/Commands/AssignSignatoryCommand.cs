using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.ClearanceSignatories.Commands
{
    public class AssignSignatoryCommand : IRequest<Guid>
    {
        public Guid DepartmentId { get; set; }
        public Guid FacultyId { get; set; }
    }
}
