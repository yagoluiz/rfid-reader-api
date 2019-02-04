using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventHubs;
using System.Text;
using Microsoft.Extensions.Logging;

namespace IoT.Function.Trigger
{
    public static class IoTTriggerFunction
    {
        [FunctionName("IoTTriggerFunction")]
        public static void Run([IoTHubTrigger("messages/events",
            Connection = "ConnectionString",
            ConsumerGroup = "$Default")]EventData message,
            ILogger log)
        {
            log.LogInformation($"C# IoT Hub trigger function processed a message: {Encoding.UTF8.GetString(message.Body.Array)}");
        }
    }
}