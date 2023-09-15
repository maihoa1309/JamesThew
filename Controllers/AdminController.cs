using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Project3.Controllers
{
    //[Authorize(Roles = "ADMIN")]
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
        public IActionResult RecipesByUser()
        {
            return View();
        }
        public IActionResult AddOrUpdateRecipe(int id)
		{
			return View(id);
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
        public IActionResult AddCategory() 
        {
            return View();
        }
        public IActionResult UpdateRecipe(int id)
        {
          

            return View(id);
        }
        public IActionResult Ingredients()
        {
            return View();
        }
	}
}
