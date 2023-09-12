using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IFeedBackRepository : IBaseRepository<Feedback>
    {
        Task<List<Feedback>> GetFeedbackByType(string word);
        
    }
    public class FeedBackRepository : BaseRepository<Feedback>, IFeedBackRepository
    {
        public FeedBackRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }

		public async Task<List<Feedback>> GetFeedbackByType(string word)
		{
            var feedbacks = from f in _context.Feedbacks
                            where f.TypeFeedback.ToLower().Equals(word.ToLower())
                            select f;
            return await feedbacks.ToListAsync();
		}

	}
}
