using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Question.Analytics.Models;
using Question.Analytics.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using Question.Analytics.Services.Contracts;

namespace Question.Analytics.Workers
{
    public class QuestionCreatedWorker : BackgroundService
    {
        private readonly ILogger<QuestionCreatedWorker> _logger;
        private readonly ConsumerConfig _consumerConfig;
        private readonly IQuestionService _questionService;

        public QuestionCreatedWorker(
            ILogger<QuestionCreatedWorker> logger,
            ConsumerConfig consumerConfig,
            IQuestionService questionService)
        {
            _logger = logger;
            _consumerConfig = consumerConfig;
            _questionService = questionService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var c = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
            {
                c.Subscribe("questionCreated");
                var cts = new CancellationTokenSource();

                try
                {
                    while (true)
                    {
                        try
                        {
                            var eventReceveid = c.Consume(cts.Token);

                            _logger.LogInformation($"Received message: {eventReceveid.Message.Value} of {eventReceveid.TopicPartitionOffset}");

                            var question = JsonSerializer.Deserialize<QuestionDetail>(eventReceveid.Message.Value);

                            _questionService.Create(question);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    c.Close();
                }
            }

            return Task.CompletedTask;
        }
    }
}
