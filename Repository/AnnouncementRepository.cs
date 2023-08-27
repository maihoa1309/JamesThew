using Project3.Models;
using Project3.Data;

namespace Project3.Repository
{
    public interface IAnnouncementRepository : IBaseRepository<FAQ>
    {

    }
    public class AnnouncementRepository : BaseRepository<FAQ>, IAnnouncementRepository
    {
        public AnnouncementRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
