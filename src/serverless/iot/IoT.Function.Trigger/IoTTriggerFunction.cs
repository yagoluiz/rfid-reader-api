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
using IoT.Function.Trigger.Enums;

namespace IoT.Function.Trigger
{
    public static class IoTTriggerFunction
    {
        [FunctionName("IoTTriggerFunction")]
        public static async Task Run([IoTHubTrigger("messages/events",
            Connection = "ConnectionString",
            ConsumerGroup = "$Default")]EventData[] events,
            ILogger log)
        {
            log.LogInformation($"EventHubTriggerFunction executed: {DateTime.Now}");

            foreach (EventData eventData in events)
            {
                var messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                var telemetryTypeModel = JsonConvert.DeserializeObject<TelemetryTypeModel>(messageBody);

                log.LogInformation($"EventHubTriggerFunction message: {messageBody}");
                log.LogInformation($"EventHubTriggerFunction telemetry type: {telemetryTypeModel.TelemetryType}");

                switch (telemetryTypeModel.TelemetryType)
                {
                    case (int)TelemetryTypeEnum.TELEMETRY:
                        var telemetryModel = JsonConvert.DeserializeObject<TelemetryModel>(messageBody);
                        await CosmosService.InsertDocumentTelemetryAsync(telemetryModel);
                        break;
                    case (int)TelemetryTypeEnum.READ:
                        var readModel = JsonConvert.DeserializeObject<ReadModel>(messageBody);
                        await CosmosService.InsertDocumentReadAsync(readModel);
                        break;
                    case (int)TelemetryTypeEnum.LOG:
                        var logName = Guid.NewGuid().ToString();
                        await StorageService.InsertBlobAsync($"{logName}.json", messageBody);
                        break;
                    default:
                        log.LogInformation($"EventHubTriggerFunction telemetry type invalid: {telemetryTypeModel.TelemetryType}");
                        break;
                }

                await Task.Yield();
            }

            log.LogInformation($"EventHubTriggerFunction finished: {DateTime.Now}");
        }
    }
}