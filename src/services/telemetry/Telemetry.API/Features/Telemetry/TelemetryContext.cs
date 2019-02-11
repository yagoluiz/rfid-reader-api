using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using System;

namespace Telemetry.API.Features.Telemetry
{
    public class TelemetryContext
    {
        private readonly IConfiguration _configuration;
        private DocumentClient _documentClient;

        public TelemetryContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DocumentClient Client
        {
            get
            {
                _documentClient = new DocumentClient(new Uri(_configuration["EndpointUriTelemetry"]), _configuration["PrimaryKeyTelemetry"]);

                return _documentClient;
            }
        }
    }
}
