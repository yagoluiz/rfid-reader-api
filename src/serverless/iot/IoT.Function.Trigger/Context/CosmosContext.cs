using Microsoft.Azure.Documents.Client;
using System;

namespace IoT.Function.Trigger.Context
{
    public static class CosmosContext
    {
        private static DocumentClient _documentClient;
        private static readonly string _endpointUri = Environment.GetEnvironmentVariable("EndpointUri", EnvironmentVariableTarget.Process);
        private static readonly string _primaryKey = Environment.GetEnvironmentVariable("PrimaryKey", EnvironmentVariableTarget.Process);

        public static DocumentClient Client
        {
            get
            {
                _documentClient = _documentClient ??
                    new DocumentClient(new Uri(_endpointUri), _primaryKey);

                return _documentClient;
            }
        }
    }
}
