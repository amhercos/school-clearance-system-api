using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scs.Application.DTOs;
using Scs.Application.Exceptions;
using Scs.Application.Features.Students.Commands;
using Scs.Application.Features.Students.Queries;

namespace SCS.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Faculty")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentDetailsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStudentDetailsAsync(Guid id)
        {
       
            var quert = new GetStudentDetailsQuery { StudentId = id };
            var result = await _mediator.Send(quert);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<StudentDetailsResponseDto>))]
        public async Task<ActionResult<IReadOnlyList<StudentDetailsResponseDto>>> GetAllStudentsAsync(CancellationToken cancellationToken)
        {
            var query = new GetAllStudentsQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteStudentCommand(id));
            return NoContent();
        }
    }
}
