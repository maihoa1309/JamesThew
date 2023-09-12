﻿using Project3.Models;

namespace Project3.DTO
{
    public class RecipeDetailDTO
    {

        public int? RecipeId { get; set; }
        public string? RecipeName { get; set; }
        public string? Username { get; set; }
        public Recipe? Recipe { get; set; } = new Recipe();
        public List<IngredientDetail>? Ingredients { get; set; } = new List<IngredientDetail>();
        public CustomUser? User { get; set; } = new CustomUser();
    }
    public class  IngredientDetail
    {
        public int? RecipeId { get; set; }  
        public int? IngredientId { get; set; }
        public string? Name { get; set; }
        public string? Quantity { get; set; }


    }
}
