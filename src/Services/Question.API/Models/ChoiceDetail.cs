using System.Text.Json.Serialization;

namespace Question.API.Models
{
    public class ChoiceDetail
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int QuestionId { get; set; }
        public string Choice { get; set; }
        public int Votes { get; set; }
    }
}
