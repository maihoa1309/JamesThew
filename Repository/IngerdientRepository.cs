using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.DTO;
using Project3.Models;
//using Project3.DTO;
using Ingredient = Project3.Models.Ingredient;
using IngredientDTO = Project3.DTO.IngredientDTO;

namespace Project3.Repository
{
    public interface IIngerdientRepository : IBaseRepository<Ingredient>
    {
		Task<List<Ingredient>> SortNameByASC();

		Task<IngredientDTO> GetByNameAsync(string keyword, int index, int size);
		Task<List<ChartData>> GetChartIngreAsync();

	}
    public class IngerdientRepository : BaseRepository<Ingredient>, IIngerdientRepository
    {
        public IngerdientRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }


        public async Task<IngredientDTO> GetByNameAsync(string keyword, int index, int size)
        {
			List<Ingredient> ingredients = await _context.Ingredients.ToListAsync();
			if (keyword != null)
			{
				ingredients = await _context.Ingredients.Where(i => i.Name.ToLower().Contains(keyword.ToLower())).ToListAsync();
			}
			var totalRow = ingredients.Count();
			ingredients = ingredients.OrderByDescending(i => i.Name).Skip((index - 1) * size).Take(size).ToList();
			IngredientDTO ingredientDTO = new IngredientDTO();
			ingredientDTO.Ingredients = ingredients;
			ingredientDTO.TotalRow = totalRow;
		
			return ingredientDTO;
        }

        public async Task<List<ChartData>> GetChartIngreAsync()
        {
			var result = await (from rd in _context.RecipeDetail
						 join i in _context.Ingredients on rd.IngredientId equals i.Id
						 join r in _context.Recipes on rd.RecipeId equals r.Id
                        group r.Id by i.Name into g
                        select new ChartData
                        {
                            Label = g.Key,
                            Data = g.Distinct().Count()
                        }).ToListAsync();	 
			return result;
        }

        public async Task<List<Ingredient>> SortNameByASC()
		{
			var result = await (from i in _context.Ingredients
						 orderby i.Name
						 where i.IsDeleted == false
						 select i).ToListAsync();
			return  result;
		}

	}
}
