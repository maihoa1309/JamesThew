using Microsoft.AspNetCore.Identity;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IContestRepository : IBaseRepository<Contest>
    {

    }
    public class ContestRepository : BaseRepository<Contest>, IContestRepository
    {
        public ContestRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }
        
    }
}
