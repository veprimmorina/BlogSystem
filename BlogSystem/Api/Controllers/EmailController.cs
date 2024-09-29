using BlogSystem.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public void NotifyUser(string toEmail, string subject, string body)
        {
            _emailService.Notify(toEmail, subject, body);
        }
    }
}
