using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scs.Application.Features.Faculties.Queries;
using Scs.Domain.Entities;

namespace SCS.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController
        : ControllerBase
    {
        private readonly IMediator _mediator;
        public FacultiesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var query = new GetAllFacultiesQuery();
            var result = await _mediator.Send(query, cancellationToken);
           return Ok(result);
        }
    }
}




