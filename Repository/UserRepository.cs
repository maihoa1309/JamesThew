using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
	public interface IUserRepository
	{

	}
	public class UserRepository : IUserRepository
	{
		protected readonly ApplicationDbContext _context;
		protected readonly UserManager<CustomUser> _userManager;
		protected readonly string currentUserId = "";
		protected readonly IHttpContextAccessor _contextAccessor;
	}
}
