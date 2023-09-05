using Microsoft.AspNetCore.Identity;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface ITipRepository : IBaseRepository<Tip>
    {

    }
    public class TipRepository : BaseRepository<Tip>, ITipRepository
    {
        public TipRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }
    }
}
