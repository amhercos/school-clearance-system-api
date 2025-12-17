using MediatR;
using Scs.Application.DTOs;

namespace Scs.Application.Features.Faculties.Queries
{
    public class GetAllFacultiesQuery : IRequest<IReadOnlyList<FacultyDto>>
    {
    }
}
