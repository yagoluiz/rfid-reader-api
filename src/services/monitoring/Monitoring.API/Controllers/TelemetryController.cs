using Microsoft.AspNetCore.Mvc;
using Monitoring.API.Services.Interfaces;
using Monitoring.API.ViewModels;
using System.Net;
using System.Threading.Tasks;

namespace Monitoring.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/telemetries")]
    public class TelemetryController : ControllerBase
    {
        private readonly ITelemetryService _telemetryService;

        public TelemetryController(ITelemetryService telemetryService)
        {
            _telemetryService = telemetryService;
        }

        /// <summary>
        /// Monitoring telemetry performed by the RFID device.
        /// </summary>
        /// <returns>List of telemetry.</returns>
        /// <response code="200">List of readings.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(TelemetryViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _telemetryService.GetAllTelmetryAsync());
        }
    }
}