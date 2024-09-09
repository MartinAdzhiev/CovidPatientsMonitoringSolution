using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Models;
using Services.Interfaces;
using System.Text;
using Utilities;

namespace AzureFunctionApplication
{
    public class TelemetryFunction
    {
        private readonly ILogger<TelemetryFunction> _logger;
        private readonly IDeviceService _deviceService;
        private readonly IPatientMeasureService _patientMeasureService;
        private readonly IDataReadingService _dataReadingService;
        private readonly IWarningService _warningService;
        private readonly IExternalApiService _externalApiService;
        public TelemetryFunction(ILogger<TelemetryFunction> logger, IDeviceService deviceService, IPatientMeasureService patientMeasureService, IDataReadingService dataReadingService, IWarningService warningService, IExternalApiService externalApiService)
        {
            _logger = logger;
            _deviceService = deviceService;
            _patientMeasureService = patientMeasureService;
            _dataReadingService = dataReadingService;
            _warningService = warningService;
            _externalApiService = externalApiService;
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
                var maxThreshold = 0.0;
                var minThreshold = 0.0;

                if(patientMeasureReading.MeasureType.Equals("Heart Rate"))
                {
                    maxThreshold = 140.0;
                    minThreshold = 30.0;
                }

                if(patientMeasureReading.MeasureType.Equals("Oxygen Saturation"))
                {
                    maxThreshold = 100.0;
                    minThreshold = 92.0;
                }

                await _patientMeasureService.InsertPatientMeasureAsync(patientMeasureReading.Name, patientMeasureReading.Embg, patientMeasureReading.MeasureType, minThreshold, maxThreshold, deviceId);
                patientMeasureId = await _patientMeasureService.GetPatientMeasureIdByEmbgAndMeasureTypeAsync(patientMeasureReading.Embg, patientMeasureReading.MeasureType);
            }

            patientMeasureReading.PatientMeasureId = patientMeasureId;
            await _dataReadingService.InsertDataAsync(patientMeasureReading.Value, patientMeasureReading.Time, patientMeasureReading.PatientMeasureId);
            await _externalApiService.PostDataAsync(patientMeasureReading);

            var hasWarning = await _warningService.WarningCheck(patientMeasureReading.Value, patientMeasureId);
            if (hasWarning)
            {
                var (minThreshold, maxThreshold) = await _patientMeasureService.GetThresholdsByPatientMeasureIdAsync(patientMeasureId);
                if(await _warningService.InsertWarningAsync(patientMeasureId, patientMeasureReading.Time, minThreshold, maxThreshold, patientMeasureReading.Value))
                {
                    var warning = new Warning
                    {
                        DateTime = patientMeasureReading.Time,
                        CurrentMinThreshold = minThreshold,
                        CurrentMaxThreshold = maxThreshold,
                        Value = patientMeasureReading.Value,
                        PatientMeasureId = patientMeasureId
                    };
                    await _externalApiService.PostWarningAsync(warning);
                }
            }
        }
    }
}
