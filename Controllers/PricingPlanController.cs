using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using Newtonsoft.Json;
using PayPal.Api;
using Project3.Data;
using Project3.Models;
using System;
using System.Collections.Generic;
namespace Project3.Controllers
{
	public class PricingPlanController : Controller
	{
		private readonly IConfiguration _configuration;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        public PricingPlanController(IHttpContextAccessor contextAccessor,UserManager<CustomUser> userManager,IConfiguration configuration, SignInManager<CustomUser> signInManager, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }
        public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult ProcessPayment(string amount)
		{
            int usd = 0;
            var type = "";
            //kiểm tra người dùng đăng nhập hay chưa
            if (_signInManager.IsSignedIn(User))//đã đăng nhập
            {
                //nhận tên người dùng
                var currentUser = _userManager.GetUserAsync(_contextAccessor.HttpContext.User).Result;
                var userId = currentUser.Id;
                //phân loại thanh toán
                if (amount == "10")
                {
                    usd = 30;
                    type = "month";
                }
                else if (amount == "100")
                {
                    usd = 365;
                    type = "year";
                }
                //kiểm tra ngày hết hạn sử dụng người dùng đăng kí chưa
                var abcd = _dbContext.Registers
                .Where(p => p.UserId == userId)
                .Select(p => p.DueDate)
                .Max();
                //kiểm tra xem người dùng còn đã đăng kí bh chưa
                if (abcd != null)
                { //đã từng đăng kí
                    
                    if (DateTime.Now > abcd)//khi người dùng hết hạn
                    {
                        var register = new Register
                        {
                            UserId = userId,
                            FromDate = DateTime.Now,
                            DueDate = DateTime.Now.AddDays(usd),
                            TypeMembership = type
                        };
                        TempData["RegisterData"] = JsonConvert.SerializeObject(register);
                    } else // khi người dùng còn hạn
                    {
                        var register = new Register
                        {
                            UserId = userId,
                            FromDate = abcd,
                            DueDate = abcd + TimeSpan.FromDays(usd),
                            TypeMembership = type
                        };
                        TempData["RegisterData"] = JsonConvert.SerializeObject(register);
                    }
                    }
                else //chưa từng đăng kí
                {
                    var register = new Register
                    {
                        UserId = userId,
                        FromDate = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(usd),
                        TypeMembership = type
                    };
                    //lưu dữ liệu để thêm vào db
                    TempData["RegisterData"] = JsonConvert.SerializeObject(register);

            if (payment.state.ToLower() == "approved")
			{
                if (TempData.ContainsKey("RegisterData"))
                {
                    var registerData = TempData["RegisterData"] as string;
                    var register = JsonConvert.DeserializeObject<Register>(registerData);
                    
                    _dbContext.Registers.Add(register);
                    _dbContext.SaveChanges();
                }
                TempData["WelcomeMessage"] = "PaymentSuccess";
				return Redirect("/Home/PricingPlan");
			}
			else
			{
                // Thanh toán thất bại
                TempData["WelcomeMessage"] = "PaymentFail";
				return Redirect("/Home/PricingPlan");
			}
		}
		public IActionResult PaymentCancelled()
		{
            // Xử lý khi người dùng hủy thanh toán
            
            TempData["WelcomeMessage"] = "PaymentCancelled";
			return Redirect("/Home/PricingPlan");

		}
	}
}

