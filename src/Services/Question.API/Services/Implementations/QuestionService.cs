using Microsoft.EntityFrameworkCore;
using Question.API.Infrastructure;
using Question.API.Models;
using Question.API.Services.Contracts;

namespace Question.API.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly QuestionContext _context;
        private readonly ILogger<QuestionService> _logger;
        private readonly ServiceBus.EventHandler _eventHandler;

        public QuestionService(
            QuestionContext context, 
            ILogger<QuestionService> logger,
            ServiceBus.EventHandler eventHandler)
        {
            _context = context;
            _logger = logger;
            _eventHandler = eventHandler;
        }

        public async Task<List<QuestionDetail>> GetQuestions()
        {
            return await _context.Questions
                .Include(x => x.Choices).ToListAsync();
        }

        public async Task<IEnumerable<QuestionDetail>> GetQuestions(int? limit, int? offset, string? filter)
        {
            var listWithFilter = await ApplyFilterAsync(filter);

            var listWithOffset = ApplyOffset(offset, listWithFilter);

            return ApplyLimit(limit, listWithOffset);
        }

        private async Task<List<QuestionDetail>> ApplyFilterAsync(string filter)
        {
            if (String.IsNullOrEmpty(filter)) return await GetQuestions();
            var filterInLowerCase = filter.ToLower();
            var questions = await GetQuestions();
            var questionsWithFilter = questions.FindAll(
                x => x.Question.ToLower().Contains(filterInLowerCase) ||
                x.Choices.Any(x => x.Choice.ToLower().Contains(filterInLowerCase)));
            return questionsWithFilter;
        }

        private List<QuestionDetail> ApplyOffset(int? offset, List<QuestionDetail> questions)
        {
            return offset.HasValue ? questions.Skip(offset.Value).ToList() : questions.ToList();
        }
        private List<QuestionDetail> ApplyLimit(int? limit, List<QuestionDetail> questions)
        {
            return limit.HasValue ? questions.Take(limit.Value).ToList() : questions.ToList();
        }

        public async Task<QuestionDetail> CreateQuestion(QuestionDetail questionDetail)
        {
            _context.Questions.Add(questionDetail);
            await _context.SaveChangesAsync();
            await _eventHandler.Emit("questionCreatedEvent", questionDetail);
            return _context.Questions.Include(x => x.Choices).FirstOrDefault(x => x.Question == questionDetail.Question);
        }

        public async Task<QuestionDetail> GetQuestion(int id)
        {
            return await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
