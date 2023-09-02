using BlogSystem.Core.Services;
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
        public void notifyUser(string toEmail, string subject, string body)
        {
            
            _emailService.notify(toEmail, subject, body);

        }
    }
}
