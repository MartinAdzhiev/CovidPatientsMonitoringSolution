using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Data;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Implementations;
using Services.Interfaces;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddTransient<DbContext>(provider =>
        {
            var connectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
            return new DbContext(connectionString);
        });

        services.AddTransient<IDeviceRepository, DeviceRepository>();
        services.AddTransient<IPatientMeasureRepository, PatientMeasureRepository>();
        services.AddTransient<IDataReadingRepository, DataReadingRepository>();
        services.AddTransient<IWarningRepository, WarningRepository>();


        services.AddTransient<IDeviceService, DeviceService>();
        services.AddTransient<IPatientMeasureService, PatientMeasureService>();
        services.AddTransient<IDataReadingService, DataReadingService>();
        services.AddTransient<IWarningService, WarningService>();
        services.AddTransient<IExternalApiService, ExternalApiService>();

        services.AddHttpClient("ExternalApiClient")
    .ConfigureHttpClient(client =>
    {
    var apiUri = Environment.GetEnvironmentVariable("ApiUri");
    if (string.IsNullOrWhiteSpace(apiUri))
    {
        throw new InvalidOperationException("Api Uri is not configured");
    }
    client.BaseAddress = new Uri(apiUri);
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    });
    })
    .Build();

host.Run();
