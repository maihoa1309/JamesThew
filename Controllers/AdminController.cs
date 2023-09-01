using Microsoft.AspNetCore.Mvc;

namespace Project3.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Recipes()
        {
            return View();
        }
		public IActionResult AddRecipe()
		{
			return View();
		}
        public IActionResult FeedbackWeb()
        {
            return View();
        }
	}
}
