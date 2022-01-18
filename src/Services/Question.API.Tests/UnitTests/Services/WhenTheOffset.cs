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
        private BaseTest _baseTest;

        public WhenTheOffset(BaseTest baseTest)
        {
            _baseTest = baseTest;
        }

        [Fact]
        public async Task IsGreaterThanTheRecordAmountAsync()
        {
            var questionService = new QuestionService(_baseTest.context, _baseTest.logger.Object, _baseTest.eventHandler.Object);
            var questions = await questionService.GetQuestions(0, 6, "");
            Assert.Empty(questions);
        }

        [Fact]
        public async Task ItIsLessThanTheTotalNumberOfQuestions()
        {
            var questionService = new QuestionService(_baseTest.context, _baseTest.logger.Object, _baseTest.eventHandler.Object);
            var questions = await questionService.GetQuestions(null, 2, "");
            Assert.Equal(2,questions.Count());
        }
    }
}
