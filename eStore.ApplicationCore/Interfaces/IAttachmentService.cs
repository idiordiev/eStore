using eStore.ApplicationCore.Entities;

namespace eStore.ApplicationCore.Interfaces
{
    public interface IAttachmentService
    {
        string CreateOrderInfoPdfAndReturnPath(Order order);
    }
}