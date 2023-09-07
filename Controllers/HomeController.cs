using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Project3.Models;
using Microsoft.AspNetCore.Identity;
using Project3.Data;

namespace Project3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger,RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
        }
        //public async Task<IActionResult> SeedingRoleAsync()
        //{
        //    var dbSeedRole = new DbSeedRole(_roleManager);
        //    await dbSeedRole.RoleData();
        //    return Ok("Ok");
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
       
        public IActionResult FAQ()
        {
          return View();
        }
        public IActionResult TemPlateKit()
        {
          return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
          return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}