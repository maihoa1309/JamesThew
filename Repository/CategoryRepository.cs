using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<List<Category>> SortNameByASCAsync();
        Task<bool> SaveCategoryAsync(Category request);
    }
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
		private readonly IWebHostEnvironment _hostingEnvironment;
		public CategoryRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext, IWebHostEnvironment hostingEnviroment) : base(dbContext, userManager, httpContext) 
        {
			_hostingEnvironment = hostingEnviroment;
		}

		public async Task<bool> SaveCategoryAsync(Category request)
		{
            var cate = new Category();
            if (request.Id > 0)
            {
                cate = _dbSet.Find(request.Id);
            }
			cate.Name = request.Name;
			cate.Img= UploadImageFromBase64(request.Img);
			cate.CreatedTime = DateTime.Now;
			if (request.Id > 0)
			{
				_dbSet.Update(cate);
			}else
			{
				_dbSet.Add(cate);
			}
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<List<Category>> SortNameByASCAsync()
        {
            var result = from c in _context.Categories
                         orderby c.Name
                         where c.IsDeleted == false
                         select c;
            return await result.ToListAsync();
        }
		private string UploadImageFromBase64(string imgsBase64)
		{
			string result = "";
			// Lấy đường dẫn tới thư mục wwwroot/UploadImg
			var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "UploadImg");

			// Tạo thư mục UploadImg nếu chưa tồn tại

			if (imgsBase64.StartsWith("/UploadImg"))
			{
				result += imgsBase64.TrimStart('/');
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
				result += imageUrl;
			}

			return result;
		}

	}
}
