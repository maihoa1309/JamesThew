using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;

namespace Project3.Controllers
{
	public class TeamController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		public TeamController(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IActionResult> gao()
		{
			return View();
		}
	}
}
