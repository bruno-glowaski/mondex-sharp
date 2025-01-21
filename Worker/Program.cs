using System.Net.Mail;
using Hangfire;
using MonDexSharp.Jobs.Healthcheck;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHangfire(x =>
{
    _ = x
      .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
      .UseSimpleAssemblyNameTypeSerializer()
      .UseRecommendedSerializerSettings()
      .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireDb"), new() { PrepareSchemaIfNecessary = true });
}
);
builder.Services.AddHangfireServer();
IConfigurationSection healthcheckConfig = builder.Configuration.GetSection("Healthcheck");
builder.Services.AddScoped(services =>
{
    string from = healthcheckConfig.GetValue<string>("From") ?? throw new KeyNotFoundException();
    string to = healthcheckConfig.GetValue<string>("To") ?? throw new KeyNotFoundException();
    string smtpHost = healthcheckConfig.GetSection("Smtp").GetValue<string>("Host") ?? throw new KeyNotFoundException();
    int smtpPort = healthcheckConfig.GetSection("Smtp").GetValue<int>("Port");
    return new HealthcheckJobConfiguration(new(from), new(to), new SmtpClient(smtpHost, smtpPort));
});
builder.Services.AddScoped<RecurringJobManager>();

IHost host = builder.Build();

using (IServiceScope scope = host.Services.CreateScope())
{
    string backendUri = healthcheckConfig.GetValue<string>("BackendUri") ?? throw new KeyNotFoundException();
    scope.ServiceProvider.GetRequiredService<RecurringJobManager>().AddOrUpdate<HealthcheckJob>(
        "backend_healthcheck",
        job => job.Run(backendUri),
        Cron.Hourly()
    );
}

host.Run();
