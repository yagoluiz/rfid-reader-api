using Asset.API.Extensions;
using Asset.API.Features.Asset.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Asset.API.Features.Asset
{
    [ApiController]
    [Authorize(AuthenticationSchemes = TokenAuthenticationOptions.Bearer)]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/assets")]
    public class AssetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Asset read by RFID tag
        /// </summary>
        /// <returns>Get asset</returns>
        [HttpGet("read")]
        public async Task<ActionResult<AssetItemReadGet>> Get()
        {
            return Ok(await _mediator.Send(new AssetItemReadGetRequest()));
        }
    }
}
