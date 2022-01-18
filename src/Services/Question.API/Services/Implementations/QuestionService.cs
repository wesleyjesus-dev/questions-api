using Microsoft.EntityFrameworkCore;
using Question.API.Exceptions;
using Question.API.Infrastructure;
using Question.API.Models;
using Question.API.ServiceBus;
using Question.API.Services.Contracts;

namespace Question.API.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly QuestionContext _context;
        private readonly ILogger<QuestionService> _logger;
        private readonly IEventHandler _eventHandler;

        public QuestionService(
            QuestionContext context, 
            ILogger<QuestionService> logger,
            IEventHandler eventHandler)
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
            await _eventHandler.Emit("questionCreated", questionDetail);
            return questionDetail;
        }

        public async Task<QuestionDetail> GetQuestion(int id)
        {
            var question = await _context.Questions.Include(x => x.Choices).FirstOrDefaultAsync(x => x.Id == id);
            if (question == null) throw new NotFoundException();
            return question;
        }

        public async Task<QuestionDetail> UpdateQuestion(QuestionDetail questionDetail)
        {
            var question = await GetQuestion(questionDetail.Id);

            question.Question = questionDetail.Question;
            question.ThumbUrl = questionDetail.ThumbUrl;
            question.ImageUrl = questionDetail.ImageUrl;
            
            question.Choices.Clear();
            
            questionDetail.Choices.ToList().ForEach(x =>
            {
                question.Choices.Add(x);
            });      
            
            _context.SaveChanges();

            await _eventHandler.Emit("updatedQuestion", question);

            return question;
        }

        public async Task<QuestionDetail> VoteChoice(int id, string choice)
        {
            var question = await GetQuestion(id);

            question.Choices.ToList().ForEach(x =>
            {
                if (x.Choice.ToLower().Contains(choice.ToLower())) x.Votes += 1;
            });

            _context.SaveChanges();

            await _eventHandler.Emit("votedChoice", question);

            return question;
        }
    }
}
