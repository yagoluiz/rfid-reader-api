using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Read.API.Extensions;
using Read.API.Features.Read.List;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Read.API.Features.Read
{
    [ApiController]
    [Authorize(AuthenticationSchemes = TokenAuthenticationOptions.Bearer)]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/reads")]
    public class ReadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Reads by limit count request
        /// </summary>
        /// <param name="request">Limit count request</param>
        /// <returns>List of reads</returns>
        [HttpGet("limit/{limit}")]
        public async Task<ActionResult<IEnumerable<ReadList>>> List([FromQuery]ReadListRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
