using Application.Interfaces;
using Application.ViewModels.SendMail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.SendMail
{
    public class SendMailController : BaseController
    {
        private readonly ISendMail _service;
        public SendMailController(ISendMail service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(MessageDTO message)
        {
            await _service.SendEmailAsync(message);
            return Ok("Email sent successfully");
        }
    }
}
