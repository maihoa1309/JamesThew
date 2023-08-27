using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IFAQRepository : IBaseRepository<FAQ>
    {

    }
    public class FAQRepository : BaseRepository<FAQ>, IFAQRepository
    {
        public FAQRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
