using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Question.API.Infrastructure;
using Question.API.Models;
using Question.API.ServiceBus;
using Question.API.Services.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Question.API.Tests.UnitTests.Services
{
    public class BaseTest : IDisposable
    {

        DbContextOptions<QuestionContext> options;
        public Moq.Mock<IEventHandler> eventHandler;
        public Moq.Mock<ILogger<QuestionService>> logger;

        public BaseTest()
        {
            options = new DbContextOptionsBuilder<QuestionContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            context = new QuestionContext(options);
            AddQuestionsInDatabase();
            logger = new Moq.Mock<ILogger<QuestionService>>();
            eventHandler = new Moq.Mock<IEventHandler>();

            eventHandler.Setup(x => x.Emit(It.IsAny<string>(), It.IsAny<QuestionDetail>()))
                .Returns(Task.CompletedTask);    
        }

        public QuestionContext context;

        public void AddQuestionsInDatabase()
        {
            foreach (var question in buildQuestions())
            {
                context.Questions.Add(question);
            }

            context.SaveChanges();
        }

        private List<QuestionDetail> buildQuestions()
        {
            var file = File.ReadAllText("./Resources/Questions.json");
            JsonSerializerOptions options = new JsonSerializerOptions();
#pragma warning disable CS8603 // Possible null reference return.
            return JsonSerializer.Deserialize<List<QuestionDetail>>(file, options);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }
    }
}
