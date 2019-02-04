using Microsoft.WindowsAzure.Storage.Table;
using Read.Domain.Interfaces.Repository;
using Read.Domain.Models;
using Read.Infra.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Read.Infra.Repository
{
    public class ReadRepository : IReadRepository
    {
        private readonly StorageContext _storageContext;

        public ReadRepository(StorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public async Task<IEnumerable<ReadModel>> GetAllReadAsync()
        {
            var tableQuery = new TableQuery<ReadModel>();

            return await _storageContext.Read.ExecuteQuerySegmentedAsync(tableQuery, null);
        }
    }
}
