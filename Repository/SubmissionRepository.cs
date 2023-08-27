
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface ISubmissionRepository : IBaseRepository<Submission>
    {

    }
    public class SubmissionRepository : BaseRepository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
