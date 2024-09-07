using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Infrastructure.Data;
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

            return services;
        }
    }
}
