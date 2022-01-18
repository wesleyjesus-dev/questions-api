using Microsoft.Extensions.Logging;
using Question.API.Services.Implementations;
using System.Linq;
using Xunit;

namespace Question.API.Tests.UnitTests.Services
{
    public class WhenTheLimit : IClassFixture<BaseTest>
    {
        private BaseTest _baseTest;

        public WhenTheLimit(BaseTest baseTest)
        {
            _baseTest = baseTest;
        }

        [Fact]
        public async void IsThreeRecords()
        {
            var questionService = new QuestionService(_baseTest.context, _baseTest.logger.Object, _baseTest.eventHandler.Object);
            var questions = await questionService.GetQuestions(3, null, null);
            Assert.Equal(3, questions.Count());
        }

        [Fact]
        public async void IsNotEntered()
        {
            var questionService = new QuestionService(_baseTest.context, _baseTest.logger.Object, _baseTest.eventHandler.Object);
            var questions = await questionService.GetQuestions(null, null,"");
            Assert.Equal(4, questions.Count());
        }
    }
}
