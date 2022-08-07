using System.Threading.Tasks;

namespace eStore.ApplicationCore.Interfaces
{
    public interface IEmailService
    {
        Task SendHtmlEmailAsync(string emailTo, string title, string text);
        Task SendHtmlEmailAsync(string emailTo, string title, string text, string attachmentFilePath);
    }
}