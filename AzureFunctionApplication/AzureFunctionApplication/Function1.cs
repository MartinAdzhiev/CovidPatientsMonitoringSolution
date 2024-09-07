using System;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionApplication
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function(nameof(Function1))]
        public void Run([EventHubTrigger("eventhubinstance201102", Connection = "EventHubConnectionString")] EventData[] events)
        {
            foreach (EventData @event in events)
            {
                if (@event.SystemProperties.TryGetValue("iothub-connection-device-id", out var deviceId))
                {
                    _logger.LogInformation("Device ID: {deviceId}", deviceId);
                }
            }
        }
    }
}
