using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telemetry.API.Extensions;
using Telemetry.API.Features.Telemetry.List;

namespace Telemetry.API.Features.Telemetry
{
    [ApiController]
    [Authorize(AuthenticationSchemes = TokenAuthenticationOptions.Bearer)]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/telemetries")]
    public class TelemetryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TelemetryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Telemetries by limit count request
        /// </summary>
        /// <param name="request">Limit count request</param>
        /// <returns>List of telemetries</returns>
        [HttpGet("limit/{limit}")]
        public async Task<ActionResult<IEnumerable<TelemetryList>>> List([FromQuery]TelemetryListRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
