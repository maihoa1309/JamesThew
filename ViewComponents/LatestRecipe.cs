using Microsoft.AspNetCore.Mvc;
using Project3.DTO;
using Project3.Models;

namespace Project3.ViewComponents
{
    public class LatestRecipe : ViewComponent
    {

        public IViewComponentResult Invoke(Recipe item)
        {
            // Logic và dữ liệu của ViewComponent

            return View(item);
        }
    }
}