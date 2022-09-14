using System;
using System.IO;
using System.Linq;
using System.Reflection;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces;
using GemBox.Spreadsheet;

namespace eStore.Infrastructure.Services.Invoices
{
    public class AttachmentService : IAttachmentService
    {
        private const string InvoiceNumberCell = "F6";
        private const string InvoiceDateCell = "F4";

        private const string CompanyNameCell = "B4";
        private const string CompanyAddressCell = "B5";
        private const string CompanyCityStateZipcodeCell = "B6";
        private const string CompanyPhoneCell = "B7";
        private const string CompanyEmailCell = "B8";

        private const string BillingNameCell = "B12";
        private const string BillingAddressCell = "B13";
        private const string BillingPhoneCell = "B14";
        private const string BillingEmailCell = "B15";

        private const string ShippingClientNameCell = "D12";
        private const string ShippingAddressCell = "D13";
        private const string ShippingPhoneCell = "D14";

        private const int ItemDescriptionColumnIndex = 1;
        private const int ItemPriceColumnIndex = 4;
        private const int ItemQuantityColumnIndex = 3;
        private const int FirstItemRowIndex = 19;

        private const string DiscountValueCell = "F32";

        public string CreateInvoice(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "The order is null.");
            if (order.Customer == null)
                throw new ArgumentNullException(nameof(order.Customer), "The order customer is null");
            if (order.OrderItems == null)
                throw new ArgumentNullException(nameof(order.OrderItems), "The collection of the order items is null.");

            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            var templatePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                               "/Services/Invoices/Template.xlsx";
            var excel = ExcelFile.Load(templatePath);

            var worksheet = excel.Worksheets.First();
            worksheet.Cells[InvoiceNumberCell].Value = 121;
            worksheet.Cells[InvoiceDateCell].Value = DateTime.Now.ToShortDateString();

            worksheet.Cells[CompanyNameCell].Value = "eStore";
            worksheet.Cells[CompanyAddressCell].Value = "vul. Khreshchatyk, 12";
            worksheet.Cells[CompanyCityStateZipcodeCell].Value = "Kyiv, 02000";
            worksheet.Cells[CompanyPhoneCell].Value = "+380123456789";
            worksheet.Cells[CompanyEmailCell].Value = "support@estore.com";

            worksheet.Cells[BillingNameCell].Value = $"{order.Customer.FirstName} {order.Customer.LastName}";
            worksheet.Cells[BillingAddressCell].Value = GetFullCustomerAddress(order.Customer);
            worksheet.Cells[BillingPhoneCell].Value = order.Customer.PhoneNumber;
            worksheet.Cells[BillingEmailCell].Value = order.Customer.Email;

            worksheet.Cells[ShippingClientNameCell].Value = $"{order.Customer.FirstName} {order.Customer.LastName}";
            worksheet.Cells[ShippingAddressCell].Value = GetFullShippingAddress(order);
            worksheet.Cells[ShippingPhoneCell].Value = order.Customer.PhoneNumber;

            var rowIndex = FirstItemRowIndex;
            foreach (var orderItem in order.OrderItems)
            {
                worksheet.Cells[rowIndex, ItemDescriptionColumnIndex].Value = orderItem.Goods.Name;
                worksheet.Cells[rowIndex, ItemQuantityColumnIndex].Value = orderItem.Quantity;
                worksheet.Cells[rowIndex, ItemPriceColumnIndex].Value = orderItem.UnitPrice;
                rowIndex++;
            }

            // there are formulas in the template to calculate total
            worksheet.Calculate();
            var invoiceFileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                  $"/Invoices/{order.TimeStamp:yyyyMMdd}-{order.Customer.FirstName}-{order.Customer.LastName}-{order.Id}.pdf";
            excel.Save(invoiceFileName);
            return invoiceFileName;
        }

        private string GetFullCustomerAddress(Customer customer)
        {
            return string.Join(", ", customer.Address, customer.City, customer.Country, customer.PostalCode);
        }

        private string GetFullShippingAddress(Order order)
        {
            return string.Join(", ", order.ShippingAddress, order.ShippingCity, order.ShippingCountry,
                order.ShippingPostalCode);
        }
    }
}