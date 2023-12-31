﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models
{
    public class RecipeDetail:Base
    {
        public int? RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe GetRecipe { get; set; }
        public int? IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public Ingredient? GetIngredient { get; set; }
        public string? Quantity { get; set; }

    }
}
