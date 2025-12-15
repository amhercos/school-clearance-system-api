using MediatR;
using Scs.Application.DTOs;

namespace Scs.Application.Features.Students.Queries
{
    public class GetStudentDetailsQuery : IRequest<StudentDetailsResponseDto>
    {
        public Guid StudentId { get; set; }
    }
}
