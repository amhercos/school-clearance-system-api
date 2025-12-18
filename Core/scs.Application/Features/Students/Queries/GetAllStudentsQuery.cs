using MediatR;
using Scs.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Features.Students.Queries
{
    public class GetAllStudentsQuery : IRequest<IReadOnlyList<StudentDetailsResponseDto>>
    {
    }

}
