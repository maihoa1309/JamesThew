using Project3.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    public class Recipe : Base
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? GetCategory { get; set; }
        public int? CookingTime { get; set; }
        public int? Servings { get; set; }
        public int? FeedbackId { get; set; }
        [ForeignKey("FeedbackId")]
        public Feedback? Feedback { get; set; }
        public string? Instruction { get; set; }
        public int? UserId { get; set; }
        //[ForeignKey("UserId")]
        //public CustomUser? User { get; set; }
        public string? Cuisines { get; set; }
        public string? Img { get; set; }

        

    }
}
