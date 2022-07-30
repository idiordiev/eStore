using System.Threading.Tasks;

namespace eStore.ApplicationCore.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string emailTo, string title, string text);
        Task SendEmailAsync(string emailTo, string title, string text, string attachmentFilePath);
    }
}