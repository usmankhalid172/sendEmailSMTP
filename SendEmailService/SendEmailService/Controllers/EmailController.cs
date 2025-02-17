using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendEmailService.Service;

namespace SendEmailService.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _emailService.SendEmailAsync(request.To, request.Subject, request.Body);
            return result ? Ok("Email sent successfully") : StatusCode(500, "Failed to send email");
        }

    }
}
