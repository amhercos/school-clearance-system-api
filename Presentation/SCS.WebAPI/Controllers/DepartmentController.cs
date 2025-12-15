using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scs.Application.DTOs;
using Scs.Application.Features.Departments.Commands;
using Scs.Application.Features.Departments.Queries;

namespace SCS.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        public async Task<IActionResult> Create(CreateDepartmentCommand command, CancellationToken cancellationToken)
        {

            DepartmentDto createdDto = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), 
                new { id = createdDto.Id },
                createdDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetDepartmentByIdQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


    }
}