using System.Threading.Tasks;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces;

public interface IEmailService
{
    Task SendRegisterEmailAsync(Customer customer);
    Task SendDeactivationEmailAsync(string email);
    Task SendChangePasswordEmailAsync(string email);
    Task SendPurchaseEmailAsyncAsync(Order order, string attachmentFilePath);
}