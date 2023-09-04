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
        public string? CookingTime { get; set; }
        public string? Servings { get; set; }
        public int? FeedbackId { get; set; }
        [ForeignKey("FeedbackId")]
        public Feedback? Feedback { get; set; }
        public string? Instruction { get; set; }
        public int? UserId { get; set; }
        public string? Cuisines { get; set; }
        public string? Img { get; set; }
        public bool? IsFree { get; set; }
    }
}
