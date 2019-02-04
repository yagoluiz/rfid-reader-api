using AutoMapper;
using Monitoring.API.Common;
using Monitoring.API.Services.Interfaces;
using Monitoring.API.ViewModels;
using Monitoring.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitoring.API.Services
{
    public class TelemetryService : ITelemetryService
    {
        private readonly ITelemetryRepository _telemetryRepository;
        private readonly IMapper _mapper;

        public TelemetryService(ITelemetryRepository telemetryRepository, IMapper mapper)
        {
            _telemetryRepository = telemetryRepository;
            _mapper = mapper;
        }

        public async Task<ResponseCommon> GetAllTelmetryAsync()
        {
            var model = _mapper.Map<IEnumerable<TelemetryViewModel>>(await _telemetryRepository.GetAllTelemetryAsync());
            return new ResponseCommon(model);
        }
    }
}
