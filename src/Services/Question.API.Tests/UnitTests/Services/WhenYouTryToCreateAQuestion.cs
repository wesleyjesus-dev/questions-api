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
        private BaseTest _database;

        public WhenYouTryToCreateAQuestion(BaseTest database)
        {
            _database = database;
        }


        [Fact]
        public async Task AndReturnSuccess()
        {
            Moq.Mock<ILogger<QuestionService>> logger = new Moq.Mock<ILogger<QuestionService>>();
            var questionService = new QuestionService(_database.context, logger.Object);
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
            Moq.Mock<ILogger<QuestionService>> logger = new Moq.Mock<ILogger<QuestionService>>();
            var questionService = new QuestionService(_database.context,  logger.Object);
            var question = new QuestionDetail()
            {
                Question = "A test question",
                Choices = new ChoiceDetail[1] { null }
            };

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await questionService.CreateQuestion(question));
        }
    }
}
