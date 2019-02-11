using IoT.Function.Trigger.Context;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;

namespace IoT.Function.Trigger.Services
{
    public static class CosmosService
    {
        public static async Task InsertDocumentTelemetryAsync(object document)
        {
            await CosmosContext.TelemetryClient
                .CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri("RfidTelemetry", "Telemetry"), document);
        }

        public static async Task InsertDocumentReadAsync(object document)
        {
            await CosmosContext.ReadClient
                .CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri("RfidRead", "Read"), document);
        }
    }
}
