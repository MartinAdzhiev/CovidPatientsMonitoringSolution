using Models;
using System.Text;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Utilities;
using Services.Interfaces;

namespace AzureFunctionApplication
{
    public class TelemetryFunction
    {
        private readonly ILogger<TelemetryFunction> _logger;
        private readonly IDeviceService _deviceService;
        private readonly IPatientMeasureService _patientMeasureService;

        public TelemetryFunction(ILogger<TelemetryFunction> logger, IDeviceService deviceService, IPatientMeasureService patientMeasureService)
        {
            _logger = logger;
            _deviceService = deviceService;
            _patientMeasureService = patientMeasureService;
        }

        [Function(nameof(TelemetryFunction))]
        public async Task RunAsync([EventHubTrigger("eventhubinstance201102", Connection = "EventHubConnectionString")] EventData[] events)
        {
            foreach (var @event in events)
            {
                if (TryGetDeviceId(@event, out var deviceId))
                {
                    _logger.LogInformation("Device ID: {DeviceId}", deviceId);
                    var jsonString = Encoding.UTF8.GetString(@event.Body.ToArray());

                    var device = JsonDeserializer.Deserialize<Device>(jsonString);
                    var patientMeasureReadingObject = JsonDeserializer.Deserialize<PatientMeasureReading>(jsonString);

                    await ProcessDeviceAndSensorData(deviceId, device, patientMeasureReadingObject);
                }
                else
                {
                    _logger.LogWarning("Device ID not found in system properties or not a valid string.");
                }
            }
        }

        private static bool TryGetDeviceId(Azure.Messaging.EventHubs.EventData @event, out string? deviceId)
        {
            if (@event.SystemProperties.TryGetValue("iothub-connection-device-id", out var deviceIdObj) &&
                deviceIdObj is string id)
            {
                deviceId = id;
                return true;
            }

            deviceId = null;
            return false;
        }

        private async Task ProcessDeviceAndSensorData(string? deviceId, Device? device, PatientMeasureReading? patientMeasureReading)
        {
            if (!string.IsNullOrWhiteSpace(deviceId))
            {
                await _deviceService.InsertDeviceAsync(deviceId);

                if (patientMeasureReading != null)
                {
                    await HandlePatientMeasureReadingAsync(patientMeasureReading, deviceId);
                }
            }
            else
            {
                _logger.LogWarning("Device ID is null or empty.");
            }
        }

        private async Task HandlePatientMeasureReadingAsync(PatientMeasureReading patientMeasureReading, string deviceId)
        {
            _logger.LogInformation("Device: {DeviceId}", deviceId);
            var patientMeasureId = await _patientMeasureService.GetPatientMeasureIdByEmbgAndMeasureTypeAsync(patientMeasureReading.Embg, patientMeasureReading.MeasureType);

            if (patientMeasureId == 0)
            {
                await _patientMeasureService.InsertPatientMeasureAsync(patientMeasureReading.Name, patientMeasureReading.Embg, patientMeasureReading.MeasureType, 0.0, 100.0, deviceId);
                patientMeasureId = await _patientMeasureService.GetPatientMeasureIdByEmbgAndMeasureTypeAsync(patientMeasureReading.Embg, patientMeasureReading.MeasureType);
            }

            patientMeasureReading.PatientMeasureId = patientMeasureId;
/*            await _measureService.InsertDataAsync(sensorMeasureObject.Value, sensorMeasureObject.Time, sensorMeasureObject.Buffered, sensorId);
            await _externalApiService.PostDataAsync(sensorMeasureObject);*/

/*            var warningType = await _warningService.WarningCheck(sensorMeasureObject.Value, sensorId);
            if (warningType != WarningType.NoWarning)
            {
                await HandleWarningAsync(sensorMeasureObject, warningType);
            }*/
        }
    }
}
