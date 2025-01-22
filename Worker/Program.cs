using System.Net.Mail;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using MonDexSharp.Core.Interfaces.Repositories;
using MonDexSharp.Data.Contexts;
using MonDexSharp.Data.Repositories;
using MonDexSharp.Jobs.Healthcheck;
using MonDexSharp.Jobs.PokeApiImporter;
using PokeApiNet;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
IConfigurationSection healthcheckConfig = builder.Configuration.GetSection("Healthcheck");

builder.Services.AddDbContext<MonDexSharpDbContext>(options =>
  {
      _ = options
        .UseSqlServer(builder.Configuration.GetConnectionString("MainDb"),
          options => options.EnableRetryOnFailure()
      );
  }
);
builder.Services.AddScoped<IPokemonTypeRepository, PokemonTypeRepository>();

builder.Services.AddScoped<PokeApiClient>();
builder.Services.AddScoped(services =>
{
    string from = healthcheckConfig.GetValue<string>("From") ?? throw new KeyNotFoundException();
    string to = healthcheckConfig.GetValue<string>("To") ?? throw new KeyNotFoundException();
    string smtpHost = healthcheckConfig.GetSection("Smtp").GetValue<string>("Host") ?? throw new KeyNotFoundException();
    int smtpPort = healthcheckConfig.GetSection("Smtp").GetValue<int>("Port");
    return new HealthcheckJobConfiguration(new(from), new(to), new SmtpClient(smtpHost, smtpPort));
});

builder.Services.AddHangfire(x =>
{
    _ = x
      .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
      .UseSimpleAssemblyNameTypeSerializer()
      .UseRecommendedSerializerSettings()
      .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireDb"), new() { PrepareSchemaIfNecessary = true });
}
);
builder.Services.AddScoped<RecurringJobManager>();
builder.Services.AddHangfireServer();

IHost host = builder.Build();

using (IServiceScope scope = host.Services.CreateScope())
{
    string backendUri = healthcheckConfig.GetValue<string>("BackendUri") ?? throw new KeyNotFoundException();
    RecurringJobManager jobs = scope.ServiceProvider.GetRequiredService<RecurringJobManager>();
    jobs.AddOrUpdate<HealthcheckJob>(
        "backend_healthcheck",
        job => job.Run(backendUri),
        Cron.Hourly()
    );
    jobs.AddOrUpdate<PokeApiTypeImporterJob>(
        "import_types",
        job => job.Run(CancellationToken.None),
        "0 */2 * * *"
    );
}

host.Run();
