using Project3.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.DTO
{

    public class FormAddRecipe
    {
        public int? id { get; set; } = 0;
        public string? title { get; set; }
        public string? description { get; set; }
        public string? instruction { get; set; }
        public string? cuisines { get; set; }
        public string? servings { get; set; }
        public string? cookingTime { get; set; }
        public string? category { get; set; }
        public string? isFree { get; set; }
        public string[]? imgs { get; set; }
        public Ingredient[] ingredients { get; set; }
    }

    public class Ingredient
    {
        public string quantity { get; set; }
        public string ingredientId { get; set; }
    }

}

