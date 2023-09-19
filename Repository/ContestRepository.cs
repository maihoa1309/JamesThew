using Microsoft.AspNetCore.Identity;
using Project3.Data;
using Project3.DTO;
using Project3.Models;

namespace Project3.Repository
{
    public interface IContestRepository : IBaseRepository<Contest>
    {
        Task<List<ContestDetailDTO>> GetSubmission(int ContestId);

    }
    public class ContestRepository : BaseRepository<Contest>, IContestRepository
    {
        public ContestRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }

		public Task<List<ContestDetailDTO>> GetSubmission(int ContestId)
		{
			throw new NotImplementedException();
		}
	}
}
