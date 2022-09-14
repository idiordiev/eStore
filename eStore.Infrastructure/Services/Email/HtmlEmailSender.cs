using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace eStore.Infrastructure.Services.Email
{
    public class HtmlEmailSender : IHtmlEmailSender
    {
        private readonly SmtpClient _smtpClient;
        private readonly SmtpClientOptions _options;

        public HtmlEmailSender(SmtpClient smtpClient, IOptions<SmtpClientOptions> options)
        {
            _options = options.Value;
            _smtpClient = smtpClient;
        }
        
        public async Task SendEmailAsync(string emailTo, string subject, string body)
        {
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_options.SenderEmail, _options.SenderName);
            mailMessage.To.Add(emailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            await _smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendEmailAsync(string emailTo, string subject, string body, string attachmentFilePath)
        {
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_options.SenderEmail, _options.SenderName);
            mailMessage.To.Add(emailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Attachments.Add(new Attachment(attachmentFilePath));
            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}