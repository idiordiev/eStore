using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using eStore.ApplicationCore.Interfaces;

namespace eStore.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private const string SMTP_SERVER_ADDRESS = "smtp.sendgrid.net";
        private const int SMTP_SERVER_PORT = 587;
        private const string SMTP_CREDENTIALS_USERNAME = "apikey";
        private const string SMTP_CREDENTIALS_PASSWORD = "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";
        private const string SENDER_EMAIL = "i.diordev@gmail.com";
        private const string SENDER_NAME = "Ivan Diordiev";

        public async Task SendHtmlEmailAsync(string emailTo, string subject, string body)
        {
            SmtpClient smtpClient = GetSmtpClient();
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(SENDER_EMAIL, SENDER_NAME);
            mailMessage.To.Add(emailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            await smtpClient.SendMailAsync(mailMessage);
        }

        private SmtpClient GetSmtpClient()
        {
            SmtpClient smtpClient = new SmtpClient(host: SMTP_SERVER_ADDRESS, port: SMTP_SERVER_PORT);
            smtpClient.Credentials = new NetworkCredential(userName: SMTP_CREDENTIALS_USERNAME, password: SMTP_CREDENTIALS_PASSWORD);
            return smtpClient;
        }

        public async Task SendHtmlEmailAsync(string emailTo, string subject, string body, string attachmentFilePath)
        {
            SmtpClient smtpClient = GetSmtpClient();
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(SENDER_EMAIL, SENDER_NAME);
            mailMessage.To.Add(emailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Attachments.Add(new Attachment(attachmentFilePath));
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}