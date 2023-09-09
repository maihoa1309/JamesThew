using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Project3.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RecipesByAdmin()
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
        public IActionResult FeedbackRecipe()
        {
            return View();
        }
		public IActionResult AddIngredient()
		{
			return View();
		}

	}
}
