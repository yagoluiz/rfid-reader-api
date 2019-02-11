using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Configuration;
using Read.API.Features.Read.List;

namespace Read.API.Features.Read
{
    public class ReadRepository : IReadRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ReadContext _readContext;

        public ReadRepository(IConfiguration configuration, ReadContext readContext)
        {
            _configuration = configuration;
            _readContext = readContext;
        }

        public async Task<IEnumerable<ReadList>> GetAllByLimit(int limit = 10)
        {
            var readItems = new List<ReadList>();

            var query = _readContext.Client.CreateDocumentQuery<ReadList>(
                UriFactory.CreateDocumentCollectionUri(_configuration["Database"], _configuration["Collection"]),
                new FeedOptions { MaxItemCount = limit })
                .AsDocumentQuery();

            while (query.HasMoreResults)
            {
                var results = await query.ExecuteNextAsync<ReadList>();
                readItems.AddRange(results);

                if (readItems.Count.Equals(limit)) break;
            }

            return readItems;
        }
    }
}
