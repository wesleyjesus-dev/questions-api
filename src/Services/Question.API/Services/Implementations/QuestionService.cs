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

        public QuestionService(QuestionContext context, ILogger<QuestionService> logger)
        {
            _context = context;
            _logger = logger;
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
    }
}
