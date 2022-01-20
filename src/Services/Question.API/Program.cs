using JorgeSerrano.Json;
using Microsoft.EntityFrameworkCore;
using Question.API.Infrastructure;
using Question.API.Middlewares.Extensions;
using Question.API.ServiceBus;
using Question.API.Services.Contracts;
using Question.API.Services.Implementations;
using Question.Core.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

IWebHostEnvironment env = builder.Environment;

var config = builder.Configuration;

builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddBaseService(config);

builder.Services.AddControllers()
    .AddJsonOptions(configure =>
    {
        configure.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        configure.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<QuestionContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    

builder.Services.AddSingleton<IEventHandler, Question.API.ServiceBus.EventHandler>();

builder.Services.AddLogging();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddHealthChecks();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandler();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
    endpoints.MapControllers();
});

app.Run();
