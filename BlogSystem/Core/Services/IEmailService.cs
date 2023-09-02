namespace BlogSystem.Core.Services
{
    public interface IEmailService
    {
        public void notify(string toEmail, string subject, string body);
    }
}
