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
        private const string DeactivationEmailFilePath = "bin/Debug/net5.0/External/deactivate-account-email-template.html";
        private const string ChangePasswordEmailFilePath = "bin/Debug/net5.0/External/change-password-email-template.html";
        private const string PurchaseEmailFilePath = "bin/Debug/net5.0/External/order-email-template.html";

        public async Task SendRegisterEmailAsync(Customer customer)
        {
            var body = await File.ReadAllTextAsync(RegisterEmailFilePath);
            body = body.Replace("FIRST_NAME", customer.FirstName);
            body = body.Replace("LAST_NAME", customer.LastName);
            body = body.Replace("PHONE_NUMBER", customer.PhoneNumber);
            body = body.Replace("EMAIL_ADDRESS", customer.Email);
            await SendHtmlEmailAsync(customer.Email, "You've been successfully registered at eStore.com!", body);
        }

        public async Task SendDeactivationEmailAsync(string email)
        {
            var body = await File.ReadAllTextAsync(DeactivationEmailFilePath);
            body = body.Replace("DEACTIVATION_DATE", DateTime.Now.ToShortDateString());
            body = body.Replace("DEACTIVATION_TIME", DateTime.Now.ToShortTimeString());
            await SendHtmlEmailAsync(email, "Your account has been deactivated", body);
        }

        public async Task SendChangePasswordEmailAsync(string email)
        {
            var body = await File.ReadAllTextAsync(ChangePasswordEmailFilePath);
            body = body.Replace("CHANGING_DATE", DateTime.Now.ToShortDateString());
            body = body.Replace("CHANGING_TIME", DateTime.Now.ToShortTimeString());
            await SendHtmlEmailAsync(email, "Reset password", body);
        }

        public async Task SendPurchaseEmailAsyncAsync(Order order, string attachmentFilePath)
        {
            var body = await File.ReadAllTextAsync(PurchaseEmailFilePath);
            body = body.Replace("FIRST_NAME", order.Customer.FirstName);
            body = body.Replace("LAST_NAME", order.Customer.LastName);
            body = body.Replace("PHONE_NUMBER", order.Customer.PhoneNumber);
            body = body.Replace("CITY", order.ShippingCity);
            body = body.Replace("ADDRESS", order.ShippingAddress);
            body = body.Replace("POSTAL_CODE", order.ShippingPostalCode);
            body = body.Replace("PRICE", order.Total.ToString());
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