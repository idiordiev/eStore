using System.Threading.Tasks;

namespace eStore.Infrastructure.Services.Email
{
    public interface IHtmlEmailSender
    {
        Task SendEmailAsync(string emailTo, string title, string text);
        Task SendEmailAsync(string emailTo, string title, string text, string attachmentFilePath);
    }
}