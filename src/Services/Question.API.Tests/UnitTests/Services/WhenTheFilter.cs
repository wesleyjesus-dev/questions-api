using Microsoft.Extensions.Logging;
using Question.API.Services.Implementations;
using Xunit;

namespace Question.API.Tests.UnitTests.Services
{
    public class WhenTheFilter : IClassFixture<BaseTest>
    {
        private BaseTest _basetest;
        public WhenTheFilter(BaseTest basetest)
        {
            _basetest = basetest;
        }

        [Fact]
        public async void AndChoiceContainsTheWordCsharp()
        {
            var questionService = new QuestionService(_basetest.context, _basetest.logger.Object, _basetest.eventHandler.Object);
            var questions = await questionService.GetQuestions(null, null, "Csharp");
            Assert.Single(questions);
        }
    }
}
