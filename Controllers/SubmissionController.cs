using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;
using Project3.Repository;
using System.Configuration;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class SubmissionController : BaseController<Submission>
    {
		
		private readonly SignInManager<CustomUser> _signInManager;
		private readonly ApplicationDbContext _dbContext;
		private readonly UserManager<CustomUser> _userManager;
		private readonly IHttpContextAccessor _contextAccessor;
		public SubmissionController(IHttpContextAccessor contextAccessor, UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager, ApplicationDbContext dbContext,IBaseRepository<Submission> repository) : base(repository)
        {
			_signInManager = signInManager;
			_dbContext = dbContext;
			_userManager = userManager;
			_contextAccessor = contextAccessor;
		}
		[HttpPost("/Submission/submiss/{idt}")]
		public IActionResult submiss(int id)
		{
			int idt = Convert.ToInt32(RouteData.Values["idt"]);
			if (_signInManager.IsSignedIn(User))
			{
				var currentUser = _userManager.GetUserAsync(_contextAccessor.HttpContext.User).Result;
				var userId = currentUser.Id;
				var a = _dbContext.Submissions.Where(U => U.UserId == userId).Select(d => d.UserId).FirstOrDefault();
				if (id == 0)
				{
					return RedirectToAction("AddOrUpdateRecipe", "Home");
				}
				else
				{
					if (a != null)
					{
						return Redirect("/Home/Contest");
					}
					else
					{
						var sub = new Submission
						{
							ContestId = idt,
							UserId = userId,
							RecipeId = id,
							Status = "true"
						};
						_dbContext.Submissions.Add(sub);
						_dbContext.SaveChanges();
						return Redirect("/Home/Contest");
					}

				}

			}
			else
			{
				return Redirect("~/Identity/Account/Login");
			}

		}


	}
}