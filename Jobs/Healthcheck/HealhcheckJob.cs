using System.Net;
using System.Net.Mail;

namespace MonDexSharp.Jobs.Healthcheck;

public class HealthcheckJob(HealthcheckJobConfiguration Configuration)
{
    public async Task Run(string TargetUri)
    {
        HttpClient httpClient = new();

        HttpResponseMessage response = await httpClient.SendAsync(new(HttpMethod.Get, TargetUri));
        if (response.StatusCode == HttpStatusCode.OK &&
            await response.Content.ReadAsStringAsync() != "Healthy")
        {
            return;
        }

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
