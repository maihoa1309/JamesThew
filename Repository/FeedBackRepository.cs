using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PayPal.Api;
using Project3.Data;
using Project3.DTO;
using Project3.Models;

namespace Project3.Repository
{
    public interface IFeedBackRepository : IBaseRepository<Feedback>
    {
        Task<List<FeedbackDetailDTO>> GetFeedbackRecipeAsync(string recipe, int index, int size);

    }
    public class FeedBackRepository : BaseRepository<Feedback>, IFeedBackRepository
    {
        public FeedBackRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }

		public async Task<List<FeedbackDetailDTO>> GetFeedbackRecipeAsync(string recipe, int index, int size)
		{
            var result = await (from f in _context.Feedbacks
                          join u in _userManager.Users on f.UserId equals u.Id
						  join r in _context.Recipes on f.RecipeId equals r.Id 
                          where f.TypeFeedback == "recipe" && f.IsDeleted != true
						  select new FeedbackDetailDTO
                          {
                              FeedbackId = f.Id,
                              FeedbackContent= f.Content,
                              RecipeName = r.Title,
                              UserName = u.Name,
                              UserAvatar = u.Avatar
                          }).ToListAsync();
            if (!string.IsNullOrEmpty(recipe))
            {
                result =  result.Where(r => r.RecipeName.ToLower().Equals(recipe.ToLower())).ToList();
            }
            var totalRow = result.Count;
            result = result.Skip((index - 1) * size).Take(size).ToList();
            result[0].TotalRow = totalRow;
			return result;
		}

	}
}
