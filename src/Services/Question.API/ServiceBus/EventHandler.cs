using Confluent.Kafka;
using Newtonsoft.Json;

namespace Question.API.ServiceBus
{
    public class EventHandler : IEventHandler
    {
        private readonly ProducerConfig _config;
        public EventHandler(ProducerConfig config)
        {
            _config = config;
        }
        public async Task Emit<T>(string TopicName, T content) where T : class
        {
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                var message = JsonConvert.SerializeObject(content, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                await producer.ProduceAsync(TopicName, new Message<Null, string> { Value = message });
            };
        }
    }
}
