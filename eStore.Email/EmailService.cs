using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces;
using eStore.Email.Interfaces;

namespace eStore.Email
{
    public class EmailService : IEmailService
    {
        private readonly IHtmlEmailSender _htmlEmailSender;

        public EmailService(IHtmlEmailSender htmlEmailSender)
        {
            _htmlEmailSender = htmlEmailSender;
        }

        public async Task SendRegisterEmailAsync(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "The order is null.");
            
            var bodyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                           "/Templates/RegisterEmailTemplate.html";
            var body = await File.ReadAllTextAsync(bodyPath);
            body = body.Replace("FIRST_NAME", customer.FirstName);
            body = body.Replace("LAST_NAME", customer.LastName);
            body = body.Replace("PHONE_NUMBER", customer.PhoneNumber);
            body = body.Replace("EMAIL_ADDRESS", customer.Email);
            await _htmlEmailSender.SendEmailAsync(customer.Email, "You've been successfully registered at eStore.com!", body);
        }

        public async Task SendDeactivationEmailAsync(string email)
        {
            var bodyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                           "/Templates/DeactivateAccountEmailTemplate.html";
            var body = await File.ReadAllTextAsync(bodyPath);
            body = body.Replace("DEACTIVATION_DATE", DateTime.Now.ToShortDateString());
            body = body.Replace("DEACTIVATION_TIME", DateTime.Now.ToShortTimeString());
            await _htmlEmailSender.SendEmailAsync(email, "Your account has been deactivated", body);
        }

        public async Task SendChangePasswordEmailAsync(string email)
        {
            var bodyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                           "/Templates/ChangePasswordEmailTemplate.html";
            var body = await File.ReadAllTextAsync(bodyPath);
            body = body.Replace("CHANGING_DATE", DateTime.Now.ToShortDateString());
            body = body.Replace("CHANGING_TIME", DateTime.Now.ToShortTimeString());
            await _htmlEmailSender.SendEmailAsync(email, "You have changed password", body);
        }

        public async Task SendPurchaseEmailAsyncAsync(Order order, string attachmentFilePath)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "The order is null.");
            
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
            await _htmlEmailSender.SendEmailAsync(order.Customer.Email, "Thanks for purchase!", body, attachmentFilePath);
        }
    }
}