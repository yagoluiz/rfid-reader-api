using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telemetry.API.Features.Telemetry;

namespace Telemetry.API
{
    public class TelemetryHealthCheck : IHealthCheck
    {
        private readonly ITelemetryRepository _telemetryRepository;

        public TelemetryHealthCheck(ITelemetryRepository telemetryRepository)
        {
            _telemetryRepository = telemetryRepository;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var limit = 1;
            var telemetry = await _telemetryRepository.GetAllByLimit(limit);

            if (telemetry.Any())
            {
                return HealthCheckResult.Healthy();
            }

            return HealthCheckResult.Unhealthy();
        }
    }
}
