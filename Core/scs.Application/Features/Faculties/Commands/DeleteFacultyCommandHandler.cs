using MediatR;
using Scs.Application.Exceptions;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;


namespace Scs.Application.Features.Faculties.Commands
{
    public class DeleteFacultyCommandHandler(IFacultyRepository facultyRepository) : IRequestHandler<DeleteFacultyCommand>
    {
        public async Task Handle(DeleteFacultyCommand request, CancellationToken cancellationToken)
        {
            var faculty = await facultyRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Faculty), request.Id);
            await facultyRepository.DeleteAsync(faculty.Id, cancellationToken);

        }
    }
}
