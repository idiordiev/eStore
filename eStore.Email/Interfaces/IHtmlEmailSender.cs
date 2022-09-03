using System.Threading.Tasks;

namespace eStore.Email.Interfaces
{
    public interface IHtmlEmailSender
    {
        Task SendEmailAsync(string emailTo, string title, string text);
        Task SendEmailAsync(string emailTo, string title, string text, string attachmentFilePath);
    }
}