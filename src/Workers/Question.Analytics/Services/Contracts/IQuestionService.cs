using Question.Analytics.Models;
using System.Threading.Tasks;

namespace Question.Analytics.Services.Contracts
{
    public interface IQuestionService
    {
        Task Create(QuestionDetail? question);
    }
}
