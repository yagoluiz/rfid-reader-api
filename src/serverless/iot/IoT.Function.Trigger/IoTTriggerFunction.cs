using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventHubs;
using System.Text;
using Microsoft.Extensions.Logging;
using System;
using Newtonsoft.Json;
using IoT.Function.Trigger.Models;
using System.Threading.Tasks;
using IoT.Function.Trigger.Services;

namespace IoT.Function.Trigger
{
    public static class IoTTriggerFunction
    {
        [FunctionName("IoTTriggerFunction")]
        public static async Task Run([IoTHubTrigger("messages/events",
            Connection = "ConnectionString",
            ConsumerGroup = "$Default")]EventData message,
            ILogger log)
        {
            log.LogInformation($"EventHubTriggerFunction executed: {DateTime.Now}");

            var messageEvent = Encoding.UTF8.GetString(message.Body.Array);

            log.LogInformation($"C# IoT Hub trigger function processed a message: {messageEvent}");

            var telemetry = JsonConvert.DeserializeObject<TelemetryModel>(messageEvent);
            await CosmosService.InsertDocumentAsync(telemetry);

            log.LogInformation($"EventHubTriggerFunction finished: {DateTime.Now}");
        }
    }
}