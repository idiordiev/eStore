using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces;

namespace eStore.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private const string SmtpServerAddress = "smtp.sendgrid.net";
        private const int SmtpServerPort = 587;
        private const string SmtpCredentialsUsername = "apikey";

        private const string SenderEmail = "i.diordev@gmail.com";
        private const string SenderName = "Ivan Diordiev";

        private const string RegisterEmailFilePath = "bin/Debug/net5.0/External/register-email-template.html";
        private const string DeactivationEmailFilePath = "deactivate-account-email-template.html";
        private const string ChangePasswordEmailFilePath = "change-password-email-template.html";
        private const string PurchaseEmailFilePath = "order-email-template.html";

        public async Task SendRegisterEmailAsync(Customer customer)
        {
            var body = await File.ReadAllTextAsync(RegisterEmailFilePath);
            body = body.Replace("FIRST_NAME", customer.FirstName);
            body = body.Replace("LAST_NAME", customer.LastName);
            body = body.Replace("PHONE_NUMBER", customer.PhoneNumber);
            body = body.Replace("EMAIL_ADDRESS", customer.Email);
            await SendHtmlEmailAsync(customer.Email, "You've been successfully registered at eStore.com!", body);
        }

        public async Task SendDeactivationEmailAsync(Customer customer)
        {
            var body = await File.ReadAllTextAsync(DeactivationEmailFilePath);
            await SendHtmlEmailAsync(customer.Email, "Your account has been deactivated", body);
        }

        public async Task SendChangePasswordEmailAsync(string email, string link)
        {
            var body = await File.ReadAllTextAsync(ChangePasswordEmailFilePath);
            await SendHtmlEmailAsync(email, "Reset password", body);
        }

        public async Task SendPurchaseEmailAsyncAsync(Order order, string attachmentFilePath)
        {
            var body = await File.ReadAllTextAsync(PurchaseEmailFilePath);
            await SendHtmlEmailAsync(order.Customer.Email, "Thanks for purchase!", body, attachmentFilePath);
        }

        public async Task SendHtmlEmailAsync(string emailTo, string subject, string body)
        {
            var smtpClient = GetSmtpClient();
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(SenderEmail, SenderName);
            mailMessage.To.Add(emailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendHtmlEmailAsync(string emailTo, string subject, string body, string attachmentFilePath)
        {
            var smtpClient = GetSmtpClient();
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(SenderEmail, SenderName);
            mailMessage.To.Add(emailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Attachments.Add(new Attachment(attachmentFilePath));
            await smtpClient.SendMailAsync(mailMessage);
        }

        private SmtpClient GetSmtpClient()
        {
            var smtpCredentialsPassword = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var smtpClient = new SmtpClient(SmtpServerAddress, SmtpServerPort);
            smtpClient.Credentials = new NetworkCredential(SmtpCredentialsUsername, smtpCredentialsPassword);
            return smtpClient;
        }
    }
}