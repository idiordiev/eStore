using eStore.Domain.Entities;

namespace eStore.Application.Interfaces
{
    public interface IAttachmentService
    {
        string CreateInvoice(Order order);
    }
}