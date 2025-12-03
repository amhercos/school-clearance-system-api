using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scs.Application.DTOs;
using Scs.Application.Features.Users.Commands;
using Scs.Application.Features.Users.Queries;
using Scs.Domain.Entities;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator) // Dependency Injection of IMediator
    {
        _mediator = mediator;
    }

    // GET /api/users/5 (Query)
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        // Sends the query to the MediatR pipeline
        var user = await _mediator.Send(new GetUserByIdQuery(id));

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // POST /api/users (Command)
    [HttpPost]
    public async Task<ActionResult<User>> Create([FromBody] CreateUserCommand command)
    {
        // Sends the command to the MediatR pipeline
        var user = await _mediator.Send(command);

        // Return 201 Created status
        return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
    }
}