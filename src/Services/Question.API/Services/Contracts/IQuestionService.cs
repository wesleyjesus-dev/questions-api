using Question.API.Models;

namespace Question.API.Services.Contracts
{
    public interface IQuestionService
    {
        Task<QuestionDetail> VoteChoice(int id, string choice);
        Task<List<QuestionDetail>> GetQuestions();
        Task<QuestionDetail> GetQuestion(int id);
        Task<IEnumerable<QuestionDetail>> GetQuestions(int? limit, int? offset, string? filter);
        Task<QuestionDetail> CreateQuestion(QuestionDetail questionDetail);
        Task<QuestionDetail> UpdateQuestion(QuestionDetail questionDetail);

    }
}
