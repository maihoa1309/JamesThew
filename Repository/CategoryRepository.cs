using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {

    }
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }

        public List<Category> GetRandomCategories(int count)
        {
            List<Category> allCategories = _context.Categories.ToList();

            // Kiểm tra xem có đủ số lượng category hay không
            if (count >= allCategories.Count)
            {
                return allCategories; // Trả về toàn bộ danh sách category nếu yêu cầu lấy hết
            }
            else
            {
                // Sử dụng LINQ để lấy danh sách category ngẫu nhiên
                Random random = new Random();
                List<Category> randomCategories = allCategories.OrderBy(c => random.Next()).Take(count).ToList();
                return randomCategories;
            }
        }
    }
}
