using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.Faculties.Commands
{
    public record DeleteFacultyCommand (Guid Id) : IRequest;
}
