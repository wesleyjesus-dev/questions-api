namespace Question.Core.Configurations
{
    public class KafkaConfiguration
    {
        public string? GroupId { get; set; }
        public string? BootstrapServer { get; set; }
    }
}
