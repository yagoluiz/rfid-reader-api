using System.Collections.Generic;
using System.Threading.Tasks;
using Telemetry.API.Features.Telemetry.List;

namespace Telemetry.API.Features.Telemetry
{
    public interface ITelemetryRepository
    {
        Task<IEnumerable<TelemetryList>> GetAllByLimit(int limit = 10);
    }
}
