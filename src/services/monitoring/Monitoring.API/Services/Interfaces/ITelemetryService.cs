using Monitoring.API.Common;
using System.Threading.Tasks;

namespace Monitoring.API.Services.Interfaces
{
    public interface ITelemetryService
    {
        Task<ResponseCommon> GetAllTelmetryAsync();
    }
}
