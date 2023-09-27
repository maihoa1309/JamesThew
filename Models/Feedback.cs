using Project3.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    public class Feedback : Base
    {
        public string? UserId { get; set; }
        public string? TypeFeedback { get; set; }
        public string? Content { get; set; }
        public int? RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe GetRecipe { get; set; }
    }
}
