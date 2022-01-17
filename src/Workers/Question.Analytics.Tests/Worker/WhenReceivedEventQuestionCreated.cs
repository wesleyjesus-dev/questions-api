using Microsoft.Extensions.Logging;
using Moq;
using Question.Analytics.Models;
using Question.Analytics.Repositories;
using Question.Analytics.Services.Implementations;
using Xunit;

namespace Question.Analytics.Tests.Worker
{
    public class WhenReceivedEventQuestionCreated
    {

        [Fact]
        public async void AndSaveQuestionWithSuccess()
        {
            var loggerFactory = LoggerFactory.Create(x => x.AddConsole());
            var logger = loggerFactory.CreateLogger<QuestionService>();
            Moq.Mock<IQuestionRepository> questionRepository = new Moq.Mock<IQuestionRepository>();
            var questionService = new QuestionService(questionRepository.Object, logger);
            var question = new QuestionDetail { Id = 1 };
            await questionService.Create(question);
            questionRepository.Verify(x => x.Create(It.IsAny<QuestionDetail>()));
        }
    }
}
