using Microsoft.Extensions.Logging;
using Question.API.Services.Implementations;
using Xunit;

namespace Question.API.Tests.UnitTests.Services
{
    public class WhenYouSearchAQuestion : IClassFixture<BaseTest>
    {
        private BaseTest _baseTest;
        public WhenYouSearchAQuestion(BaseTest baseTest)
        {
            _baseTest = baseTest;
        }

        [Fact]
        public async void AndFind()
        {
            var questionService = new QuestionService(_baseTest.context, _baseTest.logger.Object, _baseTest.eventHandler.Object);
            var question = await questionService.GetQuestion(1);
            Assert.NotNull(question);
        }
    }
}
