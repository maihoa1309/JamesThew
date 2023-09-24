using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.DTO;
using Project3.Models;

namespace Project3.Repository
{
	public interface IUserRepository
	{
		Task<UserDTO> GetAllAsync(string keyword, int index, int size);
		Task<bool> DeleteAsync(string id);
		Task<CustomUser> FindByIdAsync(string id);
		Task<bool> UpdateUserAsync(CustomUser req);
	}
	public class UserRepository : IUserRepository
	{
		private readonly IWebHostEnvironment _hostingEnvironment;
		protected readonly ApplicationDbContext _context;
		protected readonly UserManager<CustomUser> _userManager;
		protected readonly IHttpContextAccessor _contextAccessor;

		public UserRepository(ApplicationDbContext context, UserManager<CustomUser> userManager, IHttpContextAccessor contextAccessor, IWebHostEnvironment hostingEnvironment)
		{
			_context = context;
			_userManager = userManager;
			_contextAccessor = contextAccessor;
			_hostingEnvironment = hostingEnvironment;
		}

		public async Task<bool> DeleteAsync(string id)
		{
			var user = _userManager.Users.Where(r => r.Id.Equals(id)).FirstOrDefault();
			user.IsDeleted = true;
			await _userManager.UpdateAsync(user);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<CustomUser> FindByIdAsync(string id)
		{
			return await _userManager.Users.Where(r => r.Id.Equals(id)).FirstOrDefaultAsync();
			
		}

		public async Task<UserDTO> GetAllAsync(string keyword, int index, int size)
		{
			var users = new UserDTO();
			var result = await _userManager.Users.ToListAsync();
			users.TotalRow = result.Count;
			users.Users = result.Where(r => r.IsDeleted != true).Skip((index - 1) * size).Take(size).ToList();
			return users;
		}

		public async Task<bool> UpdateUserAsync(CustomUser req)
		{
			var user = await _userManager.Users.Where(r => r.Id.Equals(req.Id)).FirstOrDefaultAsync();
			user.Avatar = UploadImageFromBase64(req.Avatar);
			user.Id = req.Id;
			user.Name = req.Name;
			user.Age= req.Age;
			user.Gender = req.Gender;
			await _userManager.UpdateAsync(user);
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
