using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {

    }
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
