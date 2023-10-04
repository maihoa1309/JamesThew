using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.DTO;
using Project3.Models;
using System.Net.Mail;
using System.Net;

namespace Project3.Repository
{
	public interface IUserRepository
	{
		Task<UserDTO> GetAllAsync(string keyword, int index, int size);
		Task<bool> DeleteAsync(string id);
		Task<UserAccountDTO> FindByIdAsync(string id);
		Task<bool> UpdateUserAsync(UserAccountDTO req);
		Task<CustomUser> FindByEmailAsync(string email);
		Task<string> GetRoleIdAsync();
		Task<List<CustomUser>> GetAllUser();
		void CheckAndSendMails();
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

		public async Task<UserAccountDTO> FindByIdAsync(string id)
		{
			var result = new UserAccountDTO();
			var user =	await _userManager.Users.Where(r => r.Id.Equals(id)).FirstOrDefaultAsync();
			result.Id = user.Id;
			result.Age= user.Age;
			result.Avatar = user.Avatar;
			result.Gender= user.Gender;
			result.Name = user.Name;
			result.RoleId = await _context.UserRoles.Where(r => r.RoleId.Equals(id)).Select(r => r.RoleId).FirstOrDefaultAsync();
			return result;
		}

		public async Task<UserDTO> GetAllAsync(string keyword, int index, int size)
		{
			var users = new UserDTO();
			var result = await _userManager.Users.ToListAsync();
			if (!string.IsNullOrEmpty(keyword))
			{
				result = result.Where(r => r.Name.ToLower().Contains(keyword.ToLower())).ToList();
			}
			users.TotalRow = result.Count;
			users.Users = result.Where(r => r.IsDeleted != true).Skip((index - 1) * size).Take(size).ToList();
			return users;
		}

		public async Task<bool> UpdateUserAsync(UserAccountDTO req)
		{
			var user = await _userManager.Users.Where(r => r.Id.Equals(req.Id)).FirstOrDefaultAsync();
			user.Avatar = UploadImageFromBase64(req.Avatar);
			user.Id = req.Id;
			user.Name = req.Name;
			user.Age= req.Age;
			user.Gender = req.Gender;
			var userRole = _context.UserRoles.Where(r => r.UserId.Equals(user.Id)).FirstOrDefault();
			if (!string.IsNullOrEmpty(req.RoleId) && userRole == null)
			{
				string query = "INSERT INTO AspNetUserRoles (UserID, RoleID) VALUES ({0}, {1})";
				await _context.Database.ExecuteSqlRawAsync(query, req.Id, req.RoleId);
			}
			await _userManager.UpdateAsync(user);
			await _context.SaveChangesAsync();
			return true;
		}
	
        public async Task<CustomUser> FindByEmailAsync(string email)
        {
            var infor = await _userManager.FindByEmailAsync(email);
			return infor;
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

		public async Task<string> GetRoleIdAsync()
		{
			var roleId =  (from r in _context.Roles
						 select r.Id).FirstOrDefault();

			return roleId;
		}

        public async Task<List<CustomUser>> GetAllUser()
        {
			var result = await _userManager.Users.ToListAsync();
			
			return  result;
        }
        public void CheckAndSendMails()
        {
            var users = (from r in _context.Registers
                         join u in _userManager.Users on r.UserId equals u.Id
                         where r.DueDate <= DateTime.Today.AddDays(7)
                         select new RegisterDTO
                         {
                             UserId = u.Id,
                             TypeMembership = r.TypeMembership,
                             FromDate = r.FromDate,
                             DueDate = r.DueDate,
                             Email = u.Email,
                             Status = r.Status,
                             UserName = u.Name
                         }).ToList();
            foreach (var user in users)
            {
                var subject = "Registration Expiration Notice";
                string body = $"Dear {user.UserName}, \n\n" +
                            "We hope this email finds you well. We would like to inform you that your " +
                            "registration on our platform is set to expire soon. Please take note of the following details: \n\n" +
                            $"Username: {user.UserName} \n" +
                            $"Registration Date: {user.FromDate} \n" +
                            $"Expiration Date: {user.DueDate}  \n\n" +
                            "To continue enjoying the benefits of our platform, we kindly request that you renew your " +
                            "registration before the expiration date. Failure to do so may result in account deactivation and" +
                            "loss of access to our services." +
                            "Renewing your registration is quick and easy. Simply log in to your account and follow the instructions" +
                            " provided in the account renewal section. If you have any questions or need further assistance, please" +
                            " don't hesitate to reach out to our support team at " +
                            "tasteofrecipe@gmail.com.\n\n" +
                            "Thank you for being a valued member of our platform. We appreciate your continued support.\n\n" +
                            "Best regards,\n" +
                            "James Thew \n" +
                            "Taste of recipe";
                SendEmail(user.Email, subject, body);
            }
        }
        private void SendEmail(string toEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("hoa.btm.1885@aptechlearning.edu.vn"); // Thay bằng địa chỉ email thực tế của bạn
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = false;

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)) // Thay bằng thông tin SMTP server của bạn
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("hoa.btm.1885@aptechlearning.edu.vn", "Hoa130903"); // Thay bằng thông tin đăng nhập SMTP server của bạn
                    smtpClient.EnableSsl = true; // Sử dụng SSL nếu cần thiết
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
