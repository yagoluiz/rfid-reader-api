using Monitoring.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitoring.Domain.Interfaces.Repository
{
    public interface ITelemetryRepository
    {
        Task<IEnumerable<TelemetryModel>> GetAllTelemetryAsync();
    }
}
