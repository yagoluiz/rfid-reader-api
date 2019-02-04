using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Configuration;
using System;

namespace Monitoring.Infra.Context
{
    public class DeviceContext
    {
        private readonly IConfiguration _configuration;
        private readonly EventHubClient _eventHubClient;

        public DeviceContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _eventHubClient = EventHubClient.CreateFromConnectionString(new EventHubsConnectionStringBuilder(
                new Uri(_configuration.GetSection("IoTHub:CompatibleEndpoint").Value),
                _configuration.GetSection("IoTHub:CompatiblePath").Value,
                _configuration.GetSection("IoTHub:SasKeyName").Value,
                _configuration.GetSection("IoTHub:SasKey").Value)
                .ToString());
        }

        public EventHubClient Device
        {
            get
            {
                return _eventHubClient;
            }
        }
    }
}
