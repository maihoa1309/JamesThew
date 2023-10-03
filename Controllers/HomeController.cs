using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Project3.Models;
using Microsoft.AspNetCore.Identity;
using Project3.Data;
using Project3.Repository;
using Hangfire;
using Project3.DTO;
using System.Net.Mail;
using System.Net;

namespace Project3.Controllers{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IRecipeRepository _recipeRepository;
        private readonly ApplicationDbContext _context;
        protected readonly UserManager<CustomUser> _userManager;
        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, IRecipeRepository recipeRepository, ApplicationDbContext context,  UserManager<CustomUser> userManager)
		{
			_logger = logger;
			_roleManager = roleManager;
			_recipeRepository = recipeRepository;
			_context = context;
			_userManager = userManager;
		}
		//public async Task<IActionResult> SeedingRoleAsync()
		//{
		//	var dbSeedRole = new DbSeedRole(_roleManager);
		//	await dbSeedRole.RoleData();
		//	return Ok("Ok");
		//}

		public IActionResult Index()
		{

			HangfireConfig.Register();
            return View();
		}
		public IActionResult InforUser(string email)
		{
			return View(email);
		}
		public IActionResult UpdateUser()
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

		public IActionResult Contest	()
		{
			return View();
		}
		public async Task<IActionResult> SingleRecipe(int id)
		{
			var isFree = await _recipeRepository.CheckRegister(id);
			if (isFree == false)
			{
                return RedirectToAction("PricingPlan");
            }
			else
			{
                var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
                return View(recipe);
            }
            

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
		public IActionResult TemPlateKit()
		{
			return View();
		}
		public IActionResult Category(int id)
		{
			return View(id);
		}

        public IActionResult Tips()
        {
            return View();
        }
        public IActionResult MoreTips()
        {
            return View();
        }
        [HttpPost]
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		public IActionResult AddOrUpdateRecipe(int id)
		{
			return View(id);
		}
		public void CheckAndSendMails()
		{
			var users = (from r in _context.Registers 
						join u in _userManager.Users on r.UserId equals u.Id
						where r.DueDate <= DateTime.Today.AddDays(7)
						select new RegisterDTO
						{
							UserId = u.Id,
							TypeMembership = r.TypeMembership,
							FromDate = r.FromDate,
							DueDate = r.DueDate,
							Email = u.Email,
							Status = r.Status,
							UserName= u.Name
						}).ToList();
			foreach (var user in users)
			{
				var subject = "Registration Expiration Notice";
                string body = $"Dear {user.UserName}, \n\n"+
                            "We hope this email finds you well. We would like to inform you that your " +
							"registration on our platform is set to expire soon. Please take note of the following details: \n\n"+
                            $"Username: {user.UserName} \n"+
							$"Registration Date: {user.FromDate} \n"+
							$"Expiration Date: {user.DueDate}  \n\n"+
                            "To continue enjoying the benefits of our platform, we kindly request that you renew your " +
							"registration before the expiration date. Failure to do so may result in account deactivation and" +
							"loss of access to our services."+
                            "Renewing your registration is quick and easy. Simply log in to your account and follow the instructions" +
							" provided in the account renewal section. If you have any questions or need further assistance, please" +
							" don't hesitate to reach out to our support team at " +
							"tasteofrecipe@gmail.com.\n\n"+
                            "Thank you for being a valued member of our platform. We appreciate your continued support.\n\n" +
                            "Best regards,\n"+
							"James Thew \n" +
							"Taste of recipe";
                SendEmail(user.Email, subject, body);
            }
        }
        private void SendEmail(string toEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("hoa.btm.1885@aptechlearning.edu.vn"); // Thay bằng địa chỉ email thực tế của bạn
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = false;

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)) // Thay bằng thông tin SMTP server của bạn
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("hoa.btm.1885@aptechlearning.edu.vn", "Hoa130903"); // Thay bằng thông tin đăng nhập SMTP server của bạn
                    smtpClient.EnableSsl = true; // Sử dụng SSL nếu cần thiết
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
    public class HangfireConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("ConnectionStrings"); // Thay YourConnectionString bằng chuỗi kết nối của bạn

            RecurringJob.AddOrUpdate<HomeController>(x => x.CheckAndSendMails(), Cron.Daily);
        }
    }

}
