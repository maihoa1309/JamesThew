using Microsoft.AspNetCore.Identity;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IIngerdientRepository : IBaseRepository<Ingredient>
    {

    }
    public class IngerdientRepository : BaseRepository<Ingredient>, IIngerdientRepository
    {
        public IngerdientRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }
    }
}
