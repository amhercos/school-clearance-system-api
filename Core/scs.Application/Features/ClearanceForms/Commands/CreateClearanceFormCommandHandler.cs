using MediatR;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;

namespace Scs.Application.Features.ClearanceForms.Commands
{
    public class CreateClearanceFormCommandHandler : IRequestHandler<CreateClearanceFormCommand, Guid>
    {
        private readonly IClearanceFormRepository _repository;

        public CreateClearanceFormCommandHandler(IClearanceFormRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> Handle(CreateClearanceFormCommand request, CancellationToken cancellationToken)
        {
            var entity = ClearanceForm.Create(
                request.StudentId,
                request.AcademicYear,
                request.Semester,
                request.Program,
                request.YearLevel
            );

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return entity.Id;

        }
    }
}
