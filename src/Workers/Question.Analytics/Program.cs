using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Question.Analytics.Repositories;
using Microsoft.Extensions.Configuration;
using Question.Analytics.Configurations;
using System;
using Question.Analytics.Workers;
using Question.Analytics.Services.Implementations;
using Question.Analytics.Services.Contracts;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {

        var kafkaConfig = config.GetRequiredSection("KafkaConfiguration").Get<KafkaConfiguration>();
        var kafkaBootstrapServerEnv = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVER");
        var bootstrapServer =
            !String.IsNullOrEmpty(kafkaBootstrapServerEnv) ? kafkaBootstrapServerEnv
            : kafkaConfig.BootstrapServer;

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddSeq();
        });

        var conf = new ConsumerConfig
        {
            GroupId = kafkaConfig.GroupId,
            BootstrapServers = bootstrapServer,
            AutoOffsetReset = AutoOffsetReset.Earliest,

        };

        services.AddSingleton<ConsumerConfig>(conf);
        services.AddSingleton<IQuestionRepository, QuestionRepository>();
        services.AddHostedService<QuestionCreatedWorker>();
        services.AddSingleton<IQuestionService, QuestionService>();
    })
    .Build();

await host.RunAsync();
