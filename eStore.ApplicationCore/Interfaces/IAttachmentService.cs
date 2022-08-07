using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;

namespace eStore.ApplicationCore.Interfaces
{
    public interface IAttachmentService
    {
        Task<string> CreateOrderInfoPdfAndReturnPathAsync(Order order);
    }
}