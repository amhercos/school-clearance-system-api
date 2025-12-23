using MediatR;

namespace Scs.Application.Features.Students.Commands
{
    public record DeleteStudentCommand(Guid Id) : IRequest;
}

