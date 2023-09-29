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
		public PricingPlanController(IHttpContextAccessor contextAccessor, UserManager<CustomUser> userManager, IConfiguration configuration, SignInManager<CustomUser> signInManager, ApplicationDbContext dbContext)
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
			var stats = "";
			
			
			int itv = _dbContext.Registers.Select(p => p.Id).Max();
			var abc = _dbContext.Registers;
			for (int i = 0; i < itv; i++)
			{
				var register = abc.FirstOrDefault(p => p.Id == i); // Tìm bản ghi có Id tương ứng

				if (register != null && register.DueDate != null && register.DueDate < DateTime.Now)
				{
					register.Status = "expired"; // Thay đổi giá trị của cột 'Status' thành "expired"
					_dbContext.SaveChanges(); // Lưu trữ thay đổi
				}
			}
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
							TypeMembership = type,
							Status = "active"
						};
						TempData["RegisterData"] = JsonConvert.SerializeObject(register);
					}
					else // khi người dùng còn hạn
					{
						var register = new Register
						{
							UserId = userId,
							FromDate = abcd,
							DueDate = abcd + TimeSpan.FromDays(usd),
							TypeMembership = type,
							Status = "active"
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
						TypeMembership = type,
						Status = "active"
					};
					//lưu dữ liệu để thêm vào db
					TempData["RegisterData"] = JsonConvert.SerializeObject(register);

				}
				//hoạt động thanh toán qua paypal 
				var apiContext = new APIContext(new OAuthTokenCredential(_configuration["AppSettings:PayPalClientId"], _configuration["AppSettings:PayPalSecretKey"]).GetAccessToken());

				var payment = Payment.Create(apiContext, new Payment
				{
					intent = "sale",
					payer = new Payer { payment_method = "paypal" },
					transactions = new List<Transaction>
				{
					new Transaction
					{
						amount = new Amount { total = amount, currency = "USD" },
						description = "Thanh toán PayPal"
					}
				},
					redirect_urls = new RedirectUrls
					{
						return_url = Url.Action("PaymentSuccess", "PricingPlan", null, Request.Scheme),
						cancel_url = Url.Action("PaymentCancelled", "PricingPlan", null, Request.Scheme)
					}
				});

				//chuyển hướng sau khi bắt đầu thanh toán
				var redirectUrl = payment.links.Find(x => x.rel == "approval_url").href;
				return Redirect(redirectUrl);
			}
			else
			{//chưa đăng nhập
				return Redirect("/Identity/Account/Login");
			}

		}

		public IActionResult PaymentSuccess(string paymentId, string token, string PayerID)
		{
			var apiContext = new APIContext(new OAuthTokenCredential(_configuration["AppSettings:PayPalClientId"], _configuration["AppSettings:PayPalSecretKey"]).GetAccessToken());

			var paymentExecution = new PaymentExecution { payer_id = PayerID };
			var payment = new Payment { id = paymentId }.Execute(apiContext, paymentExecution);

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
