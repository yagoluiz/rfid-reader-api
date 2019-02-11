using Microsoft.Azure.Documents.Client;
using System;

namespace IoT.Function.Trigger.Context
{
    public static class CosmosContext
    {
        private static DocumentClient _documentTelemetryClient;
        private static DocumentClient _documentReadClient;
        private static readonly string _endpointUriTelemetry = Environment.GetEnvironmentVariable("EndpointUriTelemetry", EnvironmentVariableTarget.Process);
        private static readonly string _primaryKeyTelemetry = Environment.GetEnvironmentVariable("PrimaryKeyTelemetry", EnvironmentVariableTarget.Process);
        private static readonly string _endpointUriRead = Environment.GetEnvironmentVariable("EndpointUriRead", EnvironmentVariableTarget.Process);
        private static readonly string _primaryKeyRead = Environment.GetEnvironmentVariable("PrimaryKeyRead", EnvironmentVariableTarget.Process);

        public static DocumentClient TelemetryClient
        {
            get
            {
                _documentTelemetryClient = _documentTelemetryClient ??
                    new DocumentClient(new Uri(_endpointUriTelemetry), _primaryKeyTelemetry);

                return _documentTelemetryClient;
            }
        }

        public static DocumentClient ReadClient
        {
            get
            {
                _documentReadClient = _documentReadClient ??
                    new DocumentClient(new Uri(_endpointUriRead), _primaryKeyRead);

                return _documentReadClient;
            }
        }
    }
}
