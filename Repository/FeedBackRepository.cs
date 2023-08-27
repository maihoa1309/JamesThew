using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IFeedBackRepository : IBaseRepository<Feedback>
    {

    }
    public class FeedBackRepository : BaseRepository<Feedback>, IFeedBackRepository
    {
        public FeedBackRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
