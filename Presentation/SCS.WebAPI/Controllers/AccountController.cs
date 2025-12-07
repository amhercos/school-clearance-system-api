using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scs.Application.Exceptions;
using Scs.Application.Features.Auth.Commands;
using Scs.Application.Features.Students.Commands; 
using Scs.Application.DTOs;

namespace Scs.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register/student")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentCommand command)
        {
            try
            {
                Guid userId = await _mediator.Send(command);
                return CreatedAtAction(nameof(RegisterStudent), new { id = userId }, userId);
            }
            catch (IdentityRegistrationException ex)
            {
                return BadRequest(new
                {
                    Message = "Registration failed due to identity requirements (e.g., duplicate email, weak password).",
                    Errors = ex.Errors
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {

            var result = await _mediator.Send(command);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return Unauthorized(new { Message = "Invalid email or password." });
        }
    }
}