using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;

namespace eStore.ApplicationCore.Interfaces
{
    public interface IEmailService
    {
        Task SendHtmlEmailAsync(string emailTo, string title, string text);
        Task SendHtmlEmailAsync(string emailTo, string title, string text, string attachmentFilePath);

        Task SendRegisterEmailAsync(Customer customer);
        Task SendDeactivationEmailAsync(Customer customer);
        Task SendChangePasswordEmailAsync(string email, string link);
        Task SendPurchaseEmailAsyncAsync(Order order, string attachmentFilePath);
    }
}