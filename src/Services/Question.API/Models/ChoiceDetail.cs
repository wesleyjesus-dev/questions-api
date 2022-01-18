

using System.Text.Json.Serialization;

namespace Question.API.Models
{
    public class ChoiceDetail
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public int Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public int QuestionId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual QuestionDetail Question { get; set; }
        public string Choice { get; set; }
        public int Votes { get; set; }
    }
}
