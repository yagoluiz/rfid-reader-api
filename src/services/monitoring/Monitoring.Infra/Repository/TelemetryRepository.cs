using Microsoft.Azure.EventHubs;
using Monitoring.Domain.Interfaces.Repository;
using Monitoring.Domain.Models;
using Monitoring.Infra.Context;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring.Infra.Repository
{
    public class TelemetryRepository : ITelemetryRepository
    {
        private readonly DeviceContext _deviceContext;

        public TelemetryRepository(DeviceContext deviceContext)
        {
            _deviceContext = deviceContext;
        }

        public async Task<IEnumerable<TelemetryModel>> GetAllTelemetryAsync()
        {
            var telemetries = new List<TelemetryModel>();

            var runtimeInformation = await _deviceContext.Device.GetRuntimeInformationAsync();
            var eventHubReceiver = _deviceContext.Device.CreateReceiver("$Default", runtimeInformation.PartitionIds.First(), EventPosition.FromEnqueuedTime(DateTime.UtcNow));

            var eventData = await eventHubReceiver.ReceiveAsync(100);

            if (eventData != null)
            {
                foreach (var telemetry in eventData)
                {
                    telemetries.Add(JsonConvert.DeserializeObject<TelemetryModel>(Encoding.UTF8.GetString(telemetry.Body)));
                }
            }

            return telemetries;
        }
    }
}
