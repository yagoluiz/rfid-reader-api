using Microsoft.Extensions.Diagnostics.HealthChecks;
using Read.API.Features.Read;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Read.API
{
    public class ReadHealthCheck : IHealthCheck
    {
        private readonly IReadRepository _readRepository;

        public ReadHealthCheck(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var limit = 1;
            var read = await _readRepository.GetAllByLimit(limit);

            if (read.Any())
            {
                return HealthCheckResult.Healthy();
            }

            return HealthCheckResult.Unhealthy();
        }
    }
}
