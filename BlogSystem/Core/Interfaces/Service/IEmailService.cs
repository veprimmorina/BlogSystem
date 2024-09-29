namespace BlogSystem.Core.Interfaces.Service
{
    public interface IEmailService
    {
        public void Notify(string toEmail, string subject, string body);
    }
}
