using Project3.Models;

namespace Project3.DTO
{
	public class ContestDetailDTO
	{
	
		public string? ContestTitle { get; set; }
		public List<SubmissionDetail>? Submissions { get; set; } = new List<SubmissionDetail>();

		public int? TotalRow { get; set; }

	}
	public class SubmissionDetail
	{
		public int? SubmissionId { get; set; }
		public int? RecipeId{ get; set; }
		public string? RecipeTitle { get; set; }
		public string? ImgRecipe { get; set; }
		public string? UserName { get; set; }
		
	}


}
