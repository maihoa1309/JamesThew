using Project3.Models;
using Project3.Data;
using Microsoft.AspNetCore.Identity;

namespace Project3.Repository
{
    public interface IAnnouncementRepository : IBaseRepository<FAQ>
    {

    }
    public class AnnouncementRepository : BaseRepository<FAQ>, IAnnouncementRepository
    {
        public AnnouncementRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }
    }
}
