using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;

namespace eStore.Email
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClientOptions _options;
        private readonly SmtpClient _smtpClient;

        public EmailService(IOptions<SmtpClientOptions> options)
        {
            _options = options.Value;
            var password = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            _smtpClient = new SmtpClient(_options.Address, _options.Port);
            _smtpClient.Credentials = new NetworkCredential(_options.UserName, password);
        }

        public async Task SendHtmlEmailAsync(string emailTo, string subject, string body)
        {
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_options.SenderEmail, _options.SenderName);
            mailMessage.To.Add(emailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            await _smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendHtmlEmailAsync(string emailTo, string subject, string body, string attachmentFilePath)
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

        public async Task SendRegisterEmailAsync(Customer customer)
        {
            var bodyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                           "/Templates/RegisterEmailTemplate.html";
            var body = await File.ReadAllTextAsync(bodyPath);
            body = body.Replace("FIRST_NAME", customer.FirstName);
            body = body.Replace("LAST_NAME", customer.LastName);
            body = body.Replace("PHONE_NUMBER", customer.PhoneNumber);
            body = body.Replace("EMAIL_ADDRESS", customer.Email);
            await SendHtmlEmailAsync(customer.Email, "You've been successfully registered at eStore.com!", body);
        }

        public async Task SendDeactivationEmailAsync(string email)
        {
            var bodyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                           "/Templates/DeactivateAccountEmailTemplate.html";
            var body = await File.ReadAllTextAsync(bodyPath);
            body = body.Replace("DEACTIVATION_DATE", DateTime.Now.ToShortDateString());
            body = body.Replace("DEACTIVATION_TIME", DateTime.Now.ToShortTimeString());
            await SendHtmlEmailAsync(email, "Your account has been deactivated", body);
        }

        public async Task SendChangePasswordEmailAsync(string email)
        {
            var bodyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                           "/Templates/ChangePasswordEmailTemplate.html";
            var body = await File.ReadAllTextAsync(bodyPath);
            body = body.Replace("CHANGING_DATE", DateTime.Now.ToShortDateString());
            body = body.Replace("CHANGING_TIME", DateTime.Now.ToShortTimeString());
            await SendHtmlEmailAsync(email, "You have changed password", body);
        }

        public async Task SendPurchaseEmailAsyncAsync(Order order, string attachmentFilePath)
        {
            var bodyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                           "/Templates/OrderEmailTemplate.html";
            var body = await File.ReadAllTextAsync(bodyPath);
            body = body.Replace("FIRST_NAME", order.Customer.FirstName);
            body = body.Replace("LAST_NAME", order.Customer.LastName);
            body = body.Replace("PHONE_NUMBER", order.Customer.PhoneNumber);
            body = body.Replace("CITY", order.ShippingCity);
            body = body.Replace("ADDRESS", order.ShippingAddress);
            body = body.Replace("POSTAL_CODE", order.ShippingPostalCode);
            body = body.Replace("PRICE", order.Total.ToString(CultureInfo.InvariantCulture));
            await SendHtmlEmailAsync(order.Customer.Email, "Thanks for purchase!", body, attachmentFilePath);
        }
    }
}