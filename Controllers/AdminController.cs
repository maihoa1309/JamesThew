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
        public IActionResult AddOrUpdateCategory(int id) 
        {
            return View(id);
        }
        public IActionResult Ingredients()
        {
            return View();
        }

        public IActionResult AddOrUpdateContest(int id)
        {

            return View(id);
        }
        public IActionResult Contests()
        {
            return View();
        }

        public IActionResult RecipesSubmitted()
        {
            return View();
        }
    

        public IActionResult Users()
        {
            return View();
        }
        public IActionResult UpdateUser(string id)
        {
            return View((object)id);
        }
        public IActionResult Tips()
        {
            return View();
        }
        public IActionResult RegisterManage()
        {
            return View();
        }
	}
}
