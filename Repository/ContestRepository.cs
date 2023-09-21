using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.DTO;
using Project3.Models;

namespace Project3.Repository
{
    public interface IContestRepository : IBaseRepository<Contest>
    {
        Task<ContestDetailDTO> GetSubmissionAsync(int ContestId, string keyword, int index, int size);

    }
    public class ContestRepository : BaseRepository<Contest>, IContestRepository
    {
        public ContestRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }

		public async Task<ContestDetailDTO> GetSubmissionAsync(int ContestId, string keyword, int index, int size)
		{
            var submissions = await (from r in _context.Recipes
                          join s in _context.Submissions on r.Id equals s.RecipeId
                          join u in _userManager.Users on r.UserId equals u.Id
                          where s.ContestId == ContestId
                          group r by new { s.Id, u.Name} into grouped
                          select new SubmissionDetail
                          {
                              SubmissionId = grouped.Key.Id,
                              RecipeId = grouped.Select(r => r.Id).FirstOrDefault(),
                              RecipeTitle = grouped.Select(r => r.Title).FirstOrDefault(),
                              ImgRecipe = grouped.Select(r => r.Img).FirstOrDefault(),
                              UserName = grouped.Key.Name
                          }).ToListAsync();
            if (!string.IsNullOrEmpty(keyword))
            {
                submissions = submissions.Where(r => r.RecipeTitle.ToLower().Contains(keyword.ToLower())).ToList();
            }
            ContestDetailDTO result = new ContestDetailDTO();
            result.ContestTitle = await (from c in _context.Contests
                                  where c.Id == ContestId
                                  select c.Title).FirstOrDefaultAsync();
            result.Submissions = submissions;
            result.TotalRow = result.Submissions.Count();

            result.Submissions.Skip((index - 1) * size).Take(size).ToList();

			return result;
		}
	}
}
