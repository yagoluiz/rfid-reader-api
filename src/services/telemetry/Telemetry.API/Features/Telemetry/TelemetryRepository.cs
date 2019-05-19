using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telemetry.API.Features.Telemetry.List;

namespace Telemetry.API.Features.Telemetry
{
    public class TelemetryRepository : ITelemetryRepository
    {
        private readonly IConfiguration _configuration;
        private readonly TelemetryContext _telemetryContext;

        public TelemetryRepository(IConfiguration configuration, TelemetryContext telemetryContext)
        {
            _configuration = configuration;
            _telemetryContext = telemetryContext;
        }

        public async Task<IEnumerable<TelemetryList>> GetAllLastReadByLimit(int limit = 100)
        {
            var telemetryItems = new List<TelemetryList>();

            var query = _telemetryContext.DocumentClient.CreateDocumentQuery<TelemetryList>(
                UriFactory.CreateDocumentCollectionUri(_configuration["CosmosDB:Database"], _configuration["CosmosDB:Collection"]),
                $@"SELECT TOP {limit} telemetry.ip, telemetry.temperature, telemetry.isConnection
                    FROM telemetry
                    ORDER BY telemetry._ts DESC",
                new FeedOptions
                {
                    EnableCrossPartitionQuery = true,
                    MaxItemCount = -1
                })
                .AsDocumentQuery();

            while (query.HasMoreResults)
            {
                var results = await query.ExecuteNextAsync<TelemetryList>();
                telemetryItems.AddRange(results);
            }

            return telemetryItems;
        }
    }
}
