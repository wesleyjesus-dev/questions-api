using Microsoft.Extensions.Logging;
using Question.API.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Question.API.Tests.UnitTests.Services
{
    public class WhenTheOffset : IClassFixture<BaseTest>
    {
        private BaseTest _database;

        public WhenTheOffset(BaseTest database)
        {
            _database = database;
        }

        [Fact]
        public async Task IsGreaterThanTheRecordAmountAsync()
        {
            Moq.Mock<ILogger<QuestionService>> logger = new Moq.Mock<ILogger<QuestionService>>();
            var questionService = new QuestionService(_database.context, logger.Object);
            var questions = await questionService.GetQuestions(0, 6, "");
            Assert.Empty(questions);
        }

        [Fact]
        public async Task ItIsLessThanTheTotalNumberOfQuestions()
        {
            Moq.Mock<ILogger<QuestionService>> logger = new Moq.Mock<ILogger<QuestionService>>();
            var questionService = new QuestionService(_database.context, logger.Object);
            var questions = await questionService.GetQuestions(null, 2, "");
            Assert.Equal(2,questions.Count());
        }
    }
}
