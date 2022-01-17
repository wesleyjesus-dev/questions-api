using System;
using System.Collections.Generic;

namespace Question.Analytics.Models
{
    public class QuestionDetail : BaseModel
    {
        public string Question { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbUrl { get; set; }
        public DateTime PublishedAt { get; set; }
        public List<ChoiceDetail> Choices { get; set; }
    }
}
