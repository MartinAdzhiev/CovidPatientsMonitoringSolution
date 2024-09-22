using Application.Interfaces;
using Application.Service.Interfaces;
using Application.Service;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection InfrastructureServices(this IServiceCollection services, IConfiguration configuration)

        {
            var keyVaultUrl = configuration["KeyVault:Url"];
            var secretClient = new SecretClient(new Uri(keyVaultUrl ?? throw new InvalidOperationException("KeyVault URL is not configured.")), new DefaultAzureCredential());
            KeyVaultSecret dbConn = secretClient.GetSecret("DatabaseConnection");

            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(dbConn.Value,
                b => b.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName)),
                ServiceLifetime.Scoped);

            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IDeviceService, DeviceService>();

            services.AddScoped<IPatientMeasureRepository, PatientMeasureRepository>();
            services.AddScoped<IPatientMeasureService, PatientMeasureService>();

            services.AddScoped<IDataReadingService, DataReadingService>();
            services.AddScoped<IDataReadingRepository, DataReadingRepository>();

            services.AddScoped<IWarningRepository, WarningRepository>();
            services.AddScoped<IWarningService, WarningService>();

            return services;
        }
    }
}
