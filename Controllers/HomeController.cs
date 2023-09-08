using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using System.Diagnostics;

namespace Project3.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

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
			return View();
		}
		public IActionResult SingleRecipe()
		{
			return View();
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
        [HttpPost]
        
            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
