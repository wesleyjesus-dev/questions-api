using Microsoft.AspNetCore.Mvc;
using Question.API.Models;
using Question.API.Services.Contracts;

namespace Question.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly IQuestionService _questionService;

        public QuestionsController(ILogger<QuestionsController> logger,
            IQuestionService questionService)
        {
            _logger = logger;
            _questionService = questionService;
        }

        [HttpPost]
        public async Task<QuestionDetail> Post([FromBody] QuestionDetail question)
        {
            return await _questionService.CreateQuestion(question);
        }

        [HttpGet]
        public async Task<IEnumerable<QuestionDetail>> Get(
            [FromQuery] int? limit,
            [FromQuery] int? offset,
            [FromQuery] string filter)
        {
            return await _questionService.GetQuestions(limit, offset, filter);
        }
    }
}
