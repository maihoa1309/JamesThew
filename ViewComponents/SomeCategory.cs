using Microsoft.AspNetCore.Mvc;
using Project3.DTO;
using Project3.Models;

namespace Project3.ViewComponents
{
	public class SomeCategory : ViewComponent
	{

		public IViewComponentResult Invoke(CategoryDetail item)
		{
			// Logic và dữ liệu của ViewComponent

			return View(item);
		}
	}
}