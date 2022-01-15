namespace Question.API.Models
{
    public class QuestionDetail
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbUrl { get; set; }
        public DateTime PublishedAt { get; set; }
        public ICollection<ChoiceDetail> Choices { get; set; }
    }
}
