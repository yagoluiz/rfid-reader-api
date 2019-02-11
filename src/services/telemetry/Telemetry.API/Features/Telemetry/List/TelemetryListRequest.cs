using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Telemetry.API.Features.Telemetry.List
{
    public class TelemetryListRequest : IRequest<IEnumerable<TelemetryList>>
    {
        [FromRoute(Name = "limit")]
        public int Limit { get; set; }
    }
}
