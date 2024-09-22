using Application;
using Azure.Identity;
using Infrastructure.DependencyInjection;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Api.SignalRConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var keyVaultUrl = builder.Configuration["KeyVault:Url"];
if (string.IsNullOrWhiteSpace(keyVaultUrl))
{
    throw new InvalidOperationException("Key Vault URL is not configured.");
}

var keyVaultEndpoint = new Uri(keyVaultUrl);
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

builder.Services.InfrastructureServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(ApplicationAssemblyMarker).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());
KeyVaultSecret signalRConn = await secretClient.GetSecretAsync("SignalRConnectionString");
builder.Services.AddSignalR().AddAzureSignalR(signalRConn.Value);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AngularPolicy");

app.UseAuthorization();

app.MapControllers();

app.MapHub<DataReadingHub>("/liveDataReading");
app.MapHub<WarningHub>("/liveWarning");

app.Run();
