using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scs.Application.DTOs;
using Scs.Application.Features.ClearanceForms.Commands;
using Scs.Application.Features.ClearanceForms.Queries; 

namespace Scs.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClearanceFormsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClearanceFormsController(IMediator mediator)
        {
            _mediator = mediator;
        }

     
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        public async Task<IActionResult> Create(CreateClearanceFormCommand command, CancellationToken cancellationToken)
        {
            // Send the command through the MediatR pipeline
            Guid newId = await _mediator.Send(command, cancellationToken);

            // Return 201 Created with the location of the new resource
            return CreatedAtAction(nameof(GetById), new { id = newId }, newId);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClearanceFormDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            // Create the Query object
            var query = new GetClearanceFormByIdQuery { Id = id };

            // Send the query through the MediatR pipeline
            var result = await _mediator.Send(query, cancellationToken);

            // Handle not found scenario
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}