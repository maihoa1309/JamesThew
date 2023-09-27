namespace Project3.DTO
{
	public class FeedbackDetailDTO
	{
		public int? FeedbackId { get; set; }
		public string? FeedbackContent { get; set; }
		public string? RecipeName { get;set; }
		public string? UserId { get;set; }
		public string? UserName { get;set;}
		public string? UserAvatar { get; set; }
		public int? TotalRow { get; set; }
	}
}
