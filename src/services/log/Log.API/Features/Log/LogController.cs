using Log.API.Extensions;
using Log.API.Features.Log.List;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Log.API.Features.Log
{
    [ApiController]
    [Authorize(AuthenticationSchemes = TokenAuthenticationOptions.Bearer)]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]s")]
    public class LogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Logs by limit count request
        /// </summary>
        /// <param name="request">Limit count request</param>
        /// <returns>List of logs</returns>
        [HttpGet("limit/{limit}")]
        public async Task<ActionResult<IEnumerable<LogList>>> List([FromQuery]LogListRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}

