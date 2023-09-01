using Microsoft.AspNetCore.Identity;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IFeedBackRepository : IBaseRepository<Feedback>
    {

    }
    public class FeedBackRepository : BaseRepository<Feedback>, IFeedBackRepository
    {
        public FeedBackRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }
    
    }
}
