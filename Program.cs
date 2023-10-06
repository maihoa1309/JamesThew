using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Job;
using Project3.Models;
using Project3.Repository;
using Quartz;
using Quartz.Impl;

namespace Project3
{
    public class Program
    {
      
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            

            builder.Services.AddIdentity<CustomUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
			builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
			builder.Services.AddScoped<IFeedBackRepository, FeedBackRepository>();
			builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<IContestRepository, ContestRepository>();
			builder.Services.AddScoped<IIngerdientRepository, IngerdientRepository>();
			builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
			builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();
			builder.Services.AddScoped<ISubmissionRepository, SubmissionRepository>();
			builder.Services.AddScoped<ITipRepository, TipRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();

			var app = builder.Build();
            async Task ConfigureQuartzScheduler()
            {
                // Tạo một đối tượng Scheduler
                ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
                IScheduler scheduler = await schedulerFactory.GetScheduler();

                // Khởi động Scheduler
                await scheduler.Start();

                // Tạo một công việc
                IJobDetail job = JobBuilder.Create<SendMail>()
                    .WithIdentity("UpdateRole", "YourJobGroup")
                    .Build();

                // Tạo một Trigger để chạy công việc mỗi ngày lúc 12:00 PM
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("YourTriggerName", "YourTriggerGroup")
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
                    .Build();

                // Lên lịch công việc với Trigger
                await scheduler.ScheduleJob(job, trigger);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
               
            }
            else
            {
               
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=HomePage}/{id?}");
            app.MapRazorPages();
            app.Run();
        }
    }
}