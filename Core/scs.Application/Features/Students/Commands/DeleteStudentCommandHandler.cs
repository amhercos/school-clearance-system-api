using MediatR;
using Scs.Application.Exceptions;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;

namespace Scs.Application.Features.Students.Commands;

public class DeleteStudentCommandHandler(IStudentRepository studentRepository)
    : IRequestHandler<DeleteStudentCommand>
{
    public async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await studentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (student is null)
            throw new NotFoundException(nameof(Student), request.Id);

        await studentRepository.DeleteAsync(student.Id, cancellationToken);

    }
}