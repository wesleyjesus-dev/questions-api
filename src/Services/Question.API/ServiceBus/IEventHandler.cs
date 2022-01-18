namespace Question.API.ServiceBus
{
    public interface IEventHandler
    {
        Task Emit<T>(string TopicName, T content) where T : class;
    }
}
