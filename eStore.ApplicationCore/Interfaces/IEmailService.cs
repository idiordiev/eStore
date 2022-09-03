using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;

namespace eStore.ApplicationCore.Interfaces
{
    public interface IEmailService
    {
        Task SendRegisterEmailAsync(Customer customer);
        Task SendDeactivationEmailAsync(string email);
        Task SendChangePasswordEmailAsync(string email);
        Task SendPurchaseEmailAsyncAsync(Order order, string attachmentFilePath);
    }
}