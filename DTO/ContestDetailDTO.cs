using Project3.Models;

namespace Project3.DTO
{
	public class ContestDetailDTO
	{
		public int? ContestId { get; set; }

		public List<SubmissionDetail>? Submissions { get; set; } = new List<SubmissionDetail>();

		public int? TotalRow { get; set; }

	}
	public class SubmissionDetail
	{
		public int? SubmissionId { get; set; }
		public int? RecipeId{ get; set; }
		public string? RecipeName { get; set; }
		public string? UserName { get; set; }
		public string? ImgRecipe { get; set; }
	}


}
