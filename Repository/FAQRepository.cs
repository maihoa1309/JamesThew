using Microsoft.AspNetCore.Identity;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IFAQRepository : IBaseRepository<FAQ>
    {

    }
    public class FAQRepository : BaseRepository<FAQ>, IFAQRepository
    {
        public FAQRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }
    }
}
