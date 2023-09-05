using Project3.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.DTO
{
	public class RecipeDetaiDTO
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public int? CategoryId { get; set; }
		public string? CookingTime { get; set; }
		public string? Servings { get; set; }
		public string? Instruction { get; set; }
		public int? UserId { get; set; }
		public string? Cuisines { get; set; }
		public IFormFile? Img1 { get; set; }
		public string? IsFree { get; set; }
		public string? Ingredient { get; set; }
		public string? Quantity { get; set; }
	}
}
