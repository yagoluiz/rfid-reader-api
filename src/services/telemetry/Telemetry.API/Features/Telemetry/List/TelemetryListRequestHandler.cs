using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Telemetry.API.Features.Telemetry.List
{
    public class TelemetryListRequestHandler : IRequestHandler<TelemetryListRequest, IEnumerable<TelemetryList>>
    {
        private readonly ITelemetryRepository _telemetryRepository;

        public TelemetryListRequestHandler(ITelemetryRepository telemetryRepository)
        {
            _telemetryRepository = telemetryRepository;
        }

        public async Task<IEnumerable<TelemetryList>> Handle(TelemetryListRequest request, CancellationToken cancellationToken)
        {
            return await _telemetryRepository.GetAllLastReadByLimit(request.Limit);
        }
    }
}
