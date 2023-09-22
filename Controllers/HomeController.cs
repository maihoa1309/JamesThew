using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Project3.Models;
using Microsoft.AspNetCore.Identity;
using Project3.Data;
using Microsoft.EntityFrameworkCore;

namespace Project3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _dbContext;

		public HomeController(ApplicationDbContext dbContext, ILogger<HomeController> logger,RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
			_dbContext = dbContext;
		}
		//public async Task<IActionResult> SeedingRoleAsync()
		//{
		//	var dbSeedRole = new DbSeedRole(_roleManager);
		//	await dbSeedRole.RoleData();
		//	return Ok("Ok");
		//}

		public IActionResult Index()
        {
            return View();
        }

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult HomePage()
		{
			return View();
		}
		public IActionResult AboutUs()
		{
			return View();
		}
		public IActionResult ContactUS()
		{
			return View();
		}

		public IActionResult Team()
		{
			var Contests = _dbContext.Contests.ToList();
			
			return View(Contests);
		}
		public IActionResult SingleRecipe(int id)
		{
			return View(id);
		}
		public IActionResult RecipeList()
		{
			return View();
		}
		public IActionResult PricingPlan()
		{
			return View();
		}
		public IActionResult JoinContributor()
		{
			return View();
		}
		public IActionResult Feed()
		{
			return View();
		}
		public IActionResult FAQ
			()
		{
			return View();
		}
		public IActionResult TemPlateKit(int id)
		{
			var Contests = _dbContext.Contests.Where(p => p.Id == id).ToList();

			return View(Contests);
			
		}
		public IActionResult Category()
		{
			return View();
		}
        [HttpPost]
        
            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
