using Microsoft.Extensions.Logging;
using Question.Analytics.Models;
using Question.Analytics.Repositories;
using Question.Analytics.Services.Contracts;
using System.Threading.Tasks;

namespace Question.Analytics.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly ILogger<QuestionService> _logger;
        private readonly IQuestionRepository _questionRepository;
        public QuestionService(
            IQuestionRepository questionRepository,
            ILogger<QuestionService> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }
        public async Task Create(QuestionDetail? question)
        {
            _logger.LogInformation($"Question id {question.Id} received");
             await _questionRepository.Create(question);
            _logger.LogInformation($"Question id {question.Id} saved");
        }
    }
}
