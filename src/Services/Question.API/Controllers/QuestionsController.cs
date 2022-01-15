using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Question.API.Infrastructure;
using Question.API.Models;

namespace Question.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly QuestionContext _context;

        public QuestionsController(ILogger<QuestionsController> logger,
            QuestionContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<List<QuestionDetail>> Get()
        {
            return await _context.Questions.ToListAsync();
        }
    }
}
