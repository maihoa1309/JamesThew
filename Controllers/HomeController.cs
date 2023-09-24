using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Project3.Models;
using Microsoft.AspNetCore.Identity;
using Project3.Data;
using Project3.Repository;
using Newtonsoft.Json;
using static Project3.Repository.ContestRepository;

namespace Project3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IRecipeRepository _recipeRepository;
		private readonly IContestRepository _contestRepository;

        public HomeController(ILogger<HomeController> logger,RoleManager<IdentityRole> roleManager , IRecipeRepository recipeRepository, IContestRepository contestRepository)
        {
            _logger = logger;
            _roleManager = roleManager;
			_recipeRepository = recipeRepository;
			_contestRepository = contestRepository;

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

		public async Task<IActionResult> Team()
		{
			string result = await _contestRepository.Burn();
			List<Contest> contests = JsonConvert.DeserializeObject<List<Contest>>(result);

			ViewBag.Result = contests;
			string jsonResult = await _contestRepository.Bunny();
			List<Resultt> results = JsonConvert.DeserializeObject<List<Resultt>>(jsonResult);
			ViewBag.JsonResult = jsonResult;
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
		public IActionResult FAQ()
		{
			return View();
		}
		public async Task<IActionResult> TemPlateKit(int id)
		{
			string result = await _contestRepository.Burn();
			List<Contest> contests = JsonConvert.DeserializeObject<List<Contest>>(result);

			ViewBag.Result = contests.Where(p => p.Id == id);
				

			return View();
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
