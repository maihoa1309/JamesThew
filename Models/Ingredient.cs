using Project3.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    public class Ingredient : Base
    {
        public int? RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe? Recipe { get; set; }

        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public string? Unit { get; set; }

    }
}
