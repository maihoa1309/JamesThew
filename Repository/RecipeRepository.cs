using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {

    }
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }
        public List<Recipe> GetLatestCreatedRecipes(int count)
        {
            // Sử dụng LINQ để truy vấn dữ liệu và lấy danh sách bản ghi được tạo gần nhất
            List<Recipe> latestRecipes = _context.Recipes.OrderByDescending(r => r.CreatedTime)
                                                       .Take(count)
                                                       .ToList();
            return latestRecipes;
        }
    }
}
