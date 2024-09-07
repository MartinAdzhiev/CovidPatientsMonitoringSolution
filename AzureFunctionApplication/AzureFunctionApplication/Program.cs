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


        services.AddTransient<IDeviceService, DeviceService>();
        services.AddTransient<IPatientMeasureService, PatientMeasureService>();
    })
    .Build();

host.Run();
