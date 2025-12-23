using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            try
            {
                var query = new GetStudentDetailsQuery { StudentId = id };

                var result = await _mediator.Send(query);

                return Ok(result);
            }

            catch (NotFoundException ex)
            {
                return NotFound(new
                {
                    Message = ex.Message,
                    Details = $"No student profile found with ID: {id}"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred while fetching the student details.",
                    Error = ex.Message
                });
            }
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
