using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Infrastructure.Settings;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace AquaticFishECommerce.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _settings;

        public EmailService(IOptions<SmtpSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string to,string subject,string body)
        {
            var email = new MimeMessage();

            email.From.Add(
                new MailboxAddress(_settings.DisplayName,_settings.Email));

            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            email.Body = new BodyBuilder
            {
                HtmlBody = body
            }.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(_settings.Host,_settings.Port, SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(_settings.Email, _settings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
