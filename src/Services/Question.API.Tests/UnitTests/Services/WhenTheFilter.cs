using Microsoft.Extensions.Logging;
using Question.API.Models;
using Question.API.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Question.API.Tests.UnitTests.Services
{
    public class WhenTheFilter : IClassFixture<BaseTest>
    {
        private BaseTest _database;
        public WhenTheFilter(BaseTest database)
        {
            _database = database;
        }

        [Fact]
        public async void AndChoiceContainsTheWordCsharp()
        {
            Moq.Mock<ILogger<QuestionService>> logger = new Moq.Mock<ILogger<QuestionService>>();
            var questionService = new QuestionService(_database.context, logger.Object);
            var questions = await questionService.GetQuestions(null, null, "Csharp");
            Assert.Single(questions);
        }
    }
}
