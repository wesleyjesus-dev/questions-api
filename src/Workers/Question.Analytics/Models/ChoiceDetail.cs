namespace Question.Analytics.Models
{
    public class ChoiceDetail
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Choice { get; set; }
        public int Votes { get; set; }
    }
}
