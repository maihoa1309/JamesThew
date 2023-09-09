using Project3.Models;

namespace Project3.DTO
{
    public class RecipeDetailDTO
    {
        public Recipe? Recipe { get; set; }
        public List<IngredientDetail>? Ingredients { get; set; }
        public CustomUser? User{ get; set; }
    }
    public class  IngredientDetail
    {
        public int? RecipeId { get; set; }  
        public int? IngredientId { get; set; }
        public string? Name { get; set; }
        public string? Quantity { get; set; }


    }
}
