using Microsoft.AspNetCore.Identity;
using Project3.Data;
using Project3.Models;
namespace Project3.Repository
{
    public interface IRegisterRepository : IBaseRepository<Register>
    {
            
    }
    public class RegisterRepository : BaseRepository<Register>, IRegisterRepository
    {
        public RegisterRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }
    }
}
