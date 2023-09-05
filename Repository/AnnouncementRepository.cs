using Project3.Models;
using Project3.Data;
using Microsoft.AspNetCore.Identity;

namespace Project3.Repository
{
    public interface IAnnouncementRepository : IBaseRepository<Announcement>
    {

    }
    public class AnnouncementRepository : BaseRepository<Announcement>, IAnnouncementRepository
    {
        public AnnouncementRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }
    }
}
