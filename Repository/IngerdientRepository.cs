using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IIngerdientRepository : IBaseRepository<Ingredient>
    {
		Task<List<Ingredient>> SortNameByASC();
		Task<List<Ingredient>> GetByNameAsync(string keyword, int index, int size);
	
	}
    public class IngerdientRepository : BaseRepository<Ingredient>, IIngerdientRepository
    {
        public IngerdientRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }

        public async Task<List<Ingredient>> GetByNameAsync(string keyword, int index, int size)
        {
			List<Ingredient> ingredients = await _context.Ingredients.ToListAsync();
			if (keyword != null)
			{
				ingredients = await _context.Ingredients.Where(i => i.Name.ToLower().Contains(keyword.ToLower())).ToListAsync();
			}
			var totalRow = ingredients.Count();
			ingredients = ingredients.OrderByDescending(i => i.Name).Skip((index - 1) * size).Take(size).ToList();
			ingredients[0].DeletedUser =  totalRow.ToString();
			return ingredients;
        }

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
