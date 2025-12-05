using MediatR;
using Scs.Application.DTOs;

namespace Scs.Application.Features.ClearanceForms.Queries
{
    public class GetClearanceFormByIdQuery : IRequest<ClearanceFormDto>
    {
        public Guid Id { get; set; }
    }

}
