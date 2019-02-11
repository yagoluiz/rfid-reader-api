using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telemetry.API.Features.Telemetry.List;

namespace Telemetry.API.Features.Telemetry
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]s")]
    public class TelemetryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TelemetryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Telemetrys by limit count request
        /// </summary>
        /// <param name="request">Limit count request</param>
        /// <returns>List of telemetry</returns>
        [HttpGet("limit/{limit}")]
        public async Task<ActionResult<IEnumerable<TelemetryList>>> List([FromQuery]TelemetryListRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
