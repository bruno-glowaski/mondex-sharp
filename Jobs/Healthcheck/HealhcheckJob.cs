using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;

namespace MonDexSharp.Jobs.Healthcheck;

public class HealthcheckJob(ILogger<HealthcheckJob> logger, HealthcheckJobConfiguration Configuration)
{
    public async Task Run(string TargetUri)
    {
        logger.LogInformation("Starting healthcheck for URI \"{URI}\"", TargetUri);
        HttpClient httpClient = new();

        HttpResponseMessage response = await httpClient.SendAsync(new(HttpMethod.Get, TargetUri));
        if (response.StatusCode == HttpStatusCode.OK &&
            await response.Content.ReadAsStringAsync() != "Healthy")
        {
            logger.LogInformation("URI \"{URI}\" is healthy", TargetUri);
            return;
        }

        logger.LogInformation("URI \"{URI}\" is unhealthy", TargetUri);
        MailMessage message = new(
            Configuration.From,
            Configuration.To)
        {
            Subject = "Healthcheck Failure",
            Body = $"Service hosted at {TargetUri} is unhealthy."
        };
        await Configuration.SmtpClient.SendMailAsync(message);
    }
}
