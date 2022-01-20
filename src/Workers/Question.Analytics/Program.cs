using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Question.Analytics.Repositories;
using Microsoft.Extensions.Configuration;
using Question.Analytics.Workers;
using Question.Analytics.Services.Implementations;
using Question.Analytics.Services.Contracts;
using Question.Core.Services;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IQuestionRepository, QuestionRepository>();
        services.AddSingleton<IQuestionService, QuestionService>();
        services.AddBaseService(config);
        services.AddHostedService<QuestionCreatedWorker>();
    })
    .Build();

await host.RunAsync();
