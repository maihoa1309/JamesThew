using Project3.Models;

namespace Project3.DTO
{
    public class RecipeDetailDTO
    {
        public Recipe? Recipe { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
        public string? UserName { get; set; }
    }
    public class  Ingredient
    {
        public int? RecipeId { get; set; }  
        public int? IngredientId { get; set; }
        public string? Name { get; set; }
        public string? Quantity { get; set; }


    }
}
