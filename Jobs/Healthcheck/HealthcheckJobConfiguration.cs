using System.Net.Mail;

namespace MonDexSharp.Jobs.Healthcheck;

public record HealthcheckJobConfiguration(MailAddress From, MailAddress To, SmtpClient SmtpClient);
