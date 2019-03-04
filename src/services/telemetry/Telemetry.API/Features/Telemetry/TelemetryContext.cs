using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using System;

namespace Telemetry.API.Features.Telemetry
{
    public class TelemetryContext
    {
        private readonly IConfiguration _configuration;

        public TelemetryContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDocumentClient DocumentClient =>
            new DocumentClient(new Uri(_configuration["CosmosDB:EndpointUri"]), _configuration["CosmosDB:PrimaryKey"]);
    }
}
