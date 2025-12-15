using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scs.Application.DTOs;
using Scs.Application.Exceptions;
using Scs.Application.Features.Students.Queries;

namespace SCS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves detailed information for a specific student.
        /// </summary>
        /// <param name="id">The unique ID of the student (which is the ApplicationUser Id).</param>
        /// <returns>A detailed student profile DTO.</returns>
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
    }
}
