using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IIngerdientRepository : IBaseRepository<Ingredient>
    {
		Task<List<Ingredient>> SortNameByASC();
	
	}
    public class IngerdientRepository : BaseRepository<Ingredient>, IIngerdientRepository
    {
        public IngerdientRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }

		

		public async Task<List<Ingredient>> SortNameByASC()
		{
			var result = from i in _context.Ingredients
						 orderby i.Name
						 where i.IsDeleted == false
						 select i;
			return await result.ToListAsync();
		}

	}
}
