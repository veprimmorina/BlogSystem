using System.Net.Mail;
using System.Net;

namespace BlogSystem.Core.Services
{
    public class EmailService : IEmailService
    {

        public void notify(string toEmail, string subject, string body)
        {

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("order.onlinemarket@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("morinaveprim1@gmail.com", "qsabkjuklraofush"),
                EnableSsl = true,
            };

            smtpClient.Send(mailMessage);

        }
    }
}
