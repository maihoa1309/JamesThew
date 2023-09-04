using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    public class Category : Base
    {
        public string? Name { get; set; }

        public int? RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe GetRecipe { get; set; }
    }
}
