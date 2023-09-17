using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<List<Category>> SortNameByASCAsync();
    }
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }


        public async Task<List<Category>> SortNameByASCAsync()
        {
            var result = from c in _context.Categories
                         orderby c.Name
                         where c.IsDeleted == false
                         select c;
            return await result.ToListAsync();

        }

    }
}
