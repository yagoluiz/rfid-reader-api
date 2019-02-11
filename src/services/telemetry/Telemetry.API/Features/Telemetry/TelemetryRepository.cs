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

        public async Task<IEnumerable<TelemetryList>> GetAllByLimit(int limit = 10)
        {
            var telemetryItems = new List<TelemetryList>();

            var query = _telemetryContext.Client.CreateDocumentQuery<TelemetryList>(
                UriFactory.CreateDocumentCollectionUri(_configuration["Database"], _configuration["Collection"]),
                new FeedOptions { MaxItemCount = limit })
                .AsDocumentQuery();

            while (query.HasMoreResults)
            {
                var results = await query.ExecuteNextAsync<TelemetryList>();
                telemetryItems.AddRange(results);

                if (telemetryItems.Count.Equals(limit)) break;
            }

            return telemetryItems;
        }
    }
}
