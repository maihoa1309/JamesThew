using Project3.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.DTO
{
	public class FormAddRecipe
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public int? CategoryId { get; set; }
		public string? CookingTime { get; set; }
		public string? Servings { get; set; }
		public string? Instruction { get; set; }
		public string? UserId { get; set; }
		public string? Cuisines { get; set; }
		public IFormFile? Img1 { get; set; }
        public IFormFile? Img2 { get; set; }
        public IFormFile? Img3 { get; set; }
        public string? IsFree { get; set; }
		public string? Ingredient { get; set; }
		public string? Ingredient1 { get; set; }
		public string? Ingredient2 { get; set; }
		public string? Ingredient3 { get; set; }
		public string? Ingredient4 { get; set; }
		public string? Ingredient5 { get; set; }
		public string? Quantity { get; set; }
		public string? Quantity1 { get;set; }
		public string? Quantity2 { get; set; }
		public string? Quantity3 { get;set; }
		public string? Quantity4 { get;set; }
		public string? Quantity5 { get;set; }

	}
}
