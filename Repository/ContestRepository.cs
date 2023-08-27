using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IContestRepository : IBaseRepository<Contest>
    {

    }
    public class ContestRepository : BaseRepository<Contest>, IContestRepository
    {
        public ContestRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
