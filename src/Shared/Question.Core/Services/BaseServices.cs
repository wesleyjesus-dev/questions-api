using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Question.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Question.Core.Services
{
    public static class BaseServices
    {
        public static IServiceCollection AddBaseService(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddKafkaConsumerConfig(configuration);
            service.AddKafkaProducerConfig(configuration);
            service.AddDistributedLogging();

            return service;
        }

        public static IServiceCollection AddDistributedLogging(this IServiceCollection service)
        {
            service.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq();
            });

            return service;
        }

        public static IServiceCollection AddKafkaProducerConfig(this IServiceCollection service, IConfiguration configuration)
        {
            var kafkaConfig = configuration.GetRequiredSection("KafkaConfiguration").Get<KafkaConfiguration>();
            var kafkaBootstrapServerEnv = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVER");
            var bootstrapServer =
                !String.IsNullOrEmpty(kafkaBootstrapServerEnv) ? kafkaBootstrapServerEnv
                : kafkaConfig.BootstrapServer;


            var producerConfig = new ProducerConfig
            {
                ClientId = Dns.GetHostName(),
                BootstrapServers = bootstrapServer,
            };

            service.AddSingleton(producerConfig);

            return service;
        }

        public static IServiceCollection AddKafkaConsumerConfig(this IServiceCollection service, IConfiguration configuration)
        {
            var kafkaConfig = configuration.GetRequiredSection("KafkaConfiguration").Get<KafkaConfiguration>();
            var kafkaBootstrapServerEnv = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVER");
            var bootstrapServer =
                !String.IsNullOrEmpty(kafkaBootstrapServerEnv) ? kafkaBootstrapServerEnv
                : kafkaConfig.BootstrapServer;


            var consumerConfig = new ConsumerConfig
            {
                GroupId = kafkaConfig.GroupId,
                BootstrapServers = bootstrapServer,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            service.AddSingleton(consumerConfig);

            return service;
        }

    }
}
