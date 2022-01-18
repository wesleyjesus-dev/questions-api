using Question.API.Services.Contracts;
using Question.Core.Configurations;
using System.Net;
using System.Net.Mail;

namespace Question.API.Services.Implementations
{
    public class ShareService : IShareService
    {
        private readonly ILogger<ShareService> _logger;
        private readonly SmtpConfiguration _smtp;
        private readonly ServiceBus.EventHandler _eventHandler;
        public ShareService(
            SmtpConfiguration smtp, 
            ILogger<ShareService> logger,
            ServiceBus.EventHandler eventHandler)
        {
            _smtp = smtp;
            _logger = logger;
            _eventHandler = eventHandler;
        }
        public async Task ByEmail(string destinationEmail, string contentUrl)
        {
            var smtpClient = new SmtpClient(_smtp.Host)
            {
                Port = int.Parse(_smtp.Port),
                Credentials = new NetworkCredential(_smtp.Username, _smtp.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtp.Email),
                Subject = "Question API",
                Body = $"<b>Url:</b>{contentUrl}",
                IsBodyHtml = true,
            };
            _logger.LogInformation($"create email body");

            mailMessage.To.Add(destinationEmail);
            await smtpClient.SendMailAsync(mailMessage);

            await _eventHandler.Emit("updatedQuestion", 
                   new { 
                        from = _smtp.Email, 
                        to = destinationEmail ,
                        content = contentUrl
                });

            _logger.LogInformation($"send email to {destinationEmail}");
        }
    }
}
