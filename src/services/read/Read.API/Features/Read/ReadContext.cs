﻿using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using System;

namespace Read.API.Features.Read
{
    public class ReadContext
    {
        private readonly IConfiguration _configuration;

        public ReadContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDocumentClient DocumentClient =>
            new DocumentClient(new Uri(_configuration["CosmosDB:EndpointUri"]), _configuration["CosmosDB:PrimaryKey"]);

        public IMessageSender MessageSender =>
            new MessageSender(_configuration["ServiceBus:ConnectionString"], _configuration["ServiceBus:Queue"]);
    }
}
