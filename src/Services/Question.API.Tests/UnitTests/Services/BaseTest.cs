using Microsoft.EntityFrameworkCore;
using Question.API.Infrastructure;
using Question.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Question.API.Tests.UnitTests.Services
{
    public class BaseTest : IDisposable
    {

        DbContextOptions<QuestionContext> options = new DbContextOptionsBuilder<QuestionContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        public BaseTest()
        { 
            context = new QuestionContext(options);
            AddQuestionsInDatabase();
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
