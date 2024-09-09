using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using Services.Interfaces;
using System.Security.Authentication;
using System.Text;

namespace Services.Implementations
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalApiService> _logger;

        public ExternalApiService(IHttpClientFactory httpClientFactory, ILogger<ExternalApiService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("ExternalApiClient");
            _logger = logger;
        }

        public async Task PostDataAsync(PatientMeasureReading patientMeasureReading)
        {
            var jsonContent = JsonConvert.SerializeObject(patientMeasureReading);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("/api/livePatientMeasure/send", content);
                response.EnsureSuccessStatusCode();
                _logger.LogInformation("Data posted successfully.");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to post data: Request error.");
            }
            catch (AuthenticationException ex)
            {
                _logger.LogError(ex, "Failed to post data: SSL/TLS authentication error.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to post data: General error.");
            }
        }

        public async Task PostWarningAsync(Warning warning)
        {
            var jsonContent = JsonConvert.SerializeObject(warning);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("/api/liveWarning/send", content);
                response.EnsureSuccessStatusCode();
                _logger.LogInformation("Data posted successfully.");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to post data: Request error.");
            }
            catch (AuthenticationException ex)
            {
                _logger.LogError(ex, "Failed to post data: SSL/TLS authentication error.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to post data: General error.");
            }
        }
    }
}
