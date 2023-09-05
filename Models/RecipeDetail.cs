using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    public class RecipeDetail:Base
    {
        public int? RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe GetRecipe { get; set; }
        public int? IngredientId { get; set; }
        [ForeignKey("Ingredient")]
        public Ingredient? GetIngredient { get; set; }
        public int? Quantity { get; set; }
        public string? Unit { get; set; }
    }
}
