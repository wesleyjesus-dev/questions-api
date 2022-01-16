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
    public class WhenTheLimit : IClassFixture<BaseTest>
    {
        private BaseTest _database;

        public WhenTheLimit(BaseTest database)
        {
            _database = database;
        }

        [Fact]
        public async void IsThreeRecords()
        {
            Moq.Mock<ILogger<QuestionService>> logger = new Moq.Mock<ILogger<QuestionService>>();
            var questionService = new QuestionService(_database.context, logger.Object);
            var questions = await questionService.GetQuestions(3, null, null);
            Assert.Equal(3, questions.Count());
        }

        [Fact]
        public async void IsNotEntered()
        {
            Moq.Mock<ILogger<QuestionService>> logger = new Moq.Mock<ILogger<QuestionService>>();
            var questionService = new QuestionService(_database.context, logger.Object);
            var questions = await questionService.GetQuestions(null, null,"");
            Assert.Equal(4, questions.Count());
        }
    }
}
