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

        public async Task<IEnumerable<ReadEpcList>> GetAllEpc()
        {
            var readItems = new List<ReadEpcList>();

            var query = _readContext.DocumentClient.CreateDocumentQuery<ReadEpcList>(
                UriFactory.CreateDocumentCollectionUri(_configuration["CosmosDB:Database"], _configuration["CosmosDB:Collection"]),
                @"SELECT DISTINCT read.epc
                    FROM read",
                new FeedOptions
                {
                    EnableCrossPartitionQuery = true,
                    MaxItemCount = -1
                })
                .AsDocumentQuery();

            while (query.HasMoreResults)
            {
                var results = await query.ExecuteNextAsync<ReadEpcList>();
                readItems.AddRange(results);
            }

            return readItems;
        }

        public async Task<IEnumerable<ReadList>> GetAllLastReadByLimit(int limit = 10)
        {
            var readItems = new List<ReadList>();

            var query = _readContext.DocumentClient.CreateDocumentQuery<ReadList>(
                UriFactory.CreateDocumentCollectionUri(_configuration["CosmosDB:Database"], _configuration["CosmosDB:Collection"]),
                $@"SELECT TOP {limit} read.ip, read.epc, read.readDate, read.antenna
                    FROM read
                    ORDER BY read._ts DESC",
                new FeedOptions
                {
                    EnableCrossPartitionQuery = true,
                    MaxItemCount = -1
                })
                .AsDocumentQuery();

            while (query.HasMoreResults)
            {
                var results = await query.ExecuteNextAsync<ReadList>();
                readItems.AddRange(results);
            }

            return readItems;
        }
    }
}
