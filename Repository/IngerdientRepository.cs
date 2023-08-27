using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IIngerdientRepository : IBaseRepository<Ingredient>
    {

    }
    public class IngerdientRepository : BaseRepository<Ingredient>, IIngerdientRepository
    {
        public IngerdientRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
