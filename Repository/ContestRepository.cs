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
        Task<bool> SaveContestAsync(Contest request);

    }
    public class ContestRepository : BaseRepository<Contest>, IContestRepository
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ContestRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext, IWebHostEnvironment hostingEnviroment) : base(dbContext, userManager, httpContext)
        {
            _hostingEnvironment = hostingEnviroment;
        }

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

        public async Task<bool> SaveContestAsync(Contest request)
        {
            var contest = new Contest();
            if (request.Id > 0)
            {
                contest = _dbSet.Find(request.Id);
                
            }
            contest.Title = request.Title;
            contest.StartDate = request.StartDate;
            contest.EndDate = request.EndDate;
            contest.CategoryId= request.CategoryId;
            contest.Img = UploadImageFromBase64(request.Img);
            contest.CreatedTime= DateTime.Now;
            contest.Description = request.Description;
            if (request.Id > 0)
            {
                _dbSet.Update(contest);
            }else
            {
                _dbSet.Add(contest);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        private string UploadImageFromBase64(string imgsBase64)
        {
            string result = "";
            // Lấy đường dẫn tới thư mục wwwroot/UploadImg
            var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "UploadImg");

            // Tạo thư mục UploadImg nếu chưa tồn tại
          
            if (imgsBase64.StartsWith("/UploadImg"))
            {
                result+=imgsBase64.TrimStart('/');
            }
            else
            {
                // Tạo tên file duy nhất bằng guid
                var fileName = $"{Guid.NewGuid()}.jpg";
                // Giải mã base64 thành mảng byte
                var imageBytes = Convert.FromBase64String(imgsBase64.Substring(imgsBase64.IndexOf(',') + 1));
                // Tạo đường dẫn tới file ảnh
                var imagePath = Path.Combine(uploadPath, fileName);
                // Lưu file ảnh vào thư mục UploadImg
                System.IO.File.WriteAllBytes(imagePath, imageBytes);
                // Trả về đường dẫn của file ảnh đã lưu
                var imageUrl = Path.Combine("UploadImg/", fileName);
                result+=imageUrl;
            }
          
            return result;
        }


    }
}
