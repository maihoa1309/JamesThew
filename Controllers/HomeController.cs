using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Project3.Models;
using Microsoft.AspNetCore.Identity;
using Project3.Data;
using Project3.Repository;

namespace Project3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IRecipeRepository _recipeRepository;

        public HomeController(ILogger<HomeController> logger,RoleManager<IdentityRole> roleManager , IRecipeRepository recipeRepository)
        {
            _logger = logger;
            _roleManager = roleManager;
			_recipeRepository = recipeRepository;
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
		public IActionResult User()
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
			return View();
		}
		public async Task<IActionResult> SingleRecipe(int id)
		{
            var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            return View(recipe);

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
		public IActionResult TemPlateKit()
		{
			return View();
		}
		public IActionResult Category(int id)
		{
			return View(id);
		}
        [HttpPost]
        
            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
