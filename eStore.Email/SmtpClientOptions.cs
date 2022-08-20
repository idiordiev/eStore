namespace eStore.Email
{
    public class SmtpClientOptions
    {
        public const string SmtpClient = "SmtpClient";
        
        public string Address { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}