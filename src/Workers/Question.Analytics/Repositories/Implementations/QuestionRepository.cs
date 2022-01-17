using Microsoft.Extensions.Configuration;
using Question.Analytics.Models;

namespace Question.Analytics.Repositories
{
    public class QuestionRepository : BaseRepository<QuestionDetail>, IQuestionRepository
    {
        public QuestionRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
