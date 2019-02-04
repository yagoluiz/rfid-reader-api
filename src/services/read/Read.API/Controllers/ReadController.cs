using Microsoft.AspNetCore.Mvc;
using Read.API.Services.Interfaces;
using Read.API.ViewModels;
using System.Net;
using System.Threading.Tasks;

namespace Read.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/reads")]
    public class ReadController : ControllerBase
    {
        private readonly IReadService _readService;

        public ReadController(IReadService readService)
        {
            _readService = readService;
        }

        /// <summary>
        /// Readings performed by the RFID device.
        /// </summary>
        /// <returns>List of readings.</returns>
        /// <response code="200">List of readings.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ReadViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _readService.GetAllReadAsync());
        }
    }
}