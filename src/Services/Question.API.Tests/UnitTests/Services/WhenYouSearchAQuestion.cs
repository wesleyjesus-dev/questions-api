using Microsoft.Extensions.Logging;
using Question.API.Services.Implementations;
using Xunit;

namespace Question.API.Tests.UnitTests.Services
{
    public class WhenYouSearchAQuestion : IClassFixture<BaseTest>
    {
        private BaseTest _database;

        public WhenYouSearchAQuestion(BaseTest database)
        {
            _database = database;
        }

        [Fact]
        public async void AndFind()
        {
            Moq.Mock<ILogger<QuestionService>> logger = new Moq.Mock<ILogger<QuestionService>>();
            var questionService = new QuestionService(_database.context, logger.Object);
            var question = await questionService.GetQuestion(1);
            Assert.NotNull(question);
        }
    }
}
