using Microsoft.AspNetCore.Mvc;
using Question.API.Services.Contracts;

namespace Question.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShareController : ControllerBase
    {
        private readonly IShareService _shareService;
        public ShareController(IShareService shareService)
        {
            _shareService = shareService;
        }

        [HttpGet]
        public async Task<IActionResult> ByEmail([FromQuery]string destination_email, [FromQuery] string content_url)
        {
            await _shareService.ByEmail(destination_email, content_url);
            return Ok(new { status = "Ok" });
        }
    }
}
