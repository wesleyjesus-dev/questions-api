using Microsoft.Extensions.Logging;
using Question.API.Models;
using Question.API.Services.Implementations;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Question.API.Tests.UnitTests.Services
{
    public class WhenYouTryToCreateAQuestion : IClassFixture<BaseTest>
    {
        private BaseTest _baseTest;
        public WhenYouTryToCreateAQuestion(BaseTest baseTest)
        {
            _baseTest = baseTest;
        }


        [Fact]
        public async Task AndReturnSuccess()
        {
            var questionService = new QuestionService(_baseTest.context, _baseTest.logger.Object, _baseTest.eventHandler.Object);
            var question = new QuestionDetail()
            {
                Question = "A test question",
                Choices = new ChoiceDetail[1]
                {
                    new ChoiceDetail(){ Choice = "choice one" }
                }
            };
            var questionCreated = await questionService.CreateQuestion(question);
            Assert.Equal(question.Question, questionCreated.Question);
        }

        [Fact]
        public async Task AndReturnError()
        {

            var questionService = new QuestionService(_baseTest.context, _baseTest.logger.Object, _baseTest.eventHandler.Object);
            var question = new QuestionDetail()
            {
                Question = "A test question",
                Choices = new ChoiceDetail[1] { null }
            };

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await questionService.CreateQuestion(question));
        }
    }
}
