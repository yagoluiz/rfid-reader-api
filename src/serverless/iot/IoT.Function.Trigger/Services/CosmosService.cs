using IoT.Function.Trigger.Context;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;

namespace IoT.Function.Trigger.Services
{
    public static class CosmosService
    {
        public static async Task InsertDocumentAsync(object document)
        {
            await CosmosContext.Client
                .CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri("Rfid", "Telemetry"), document);
        }
    }
}
