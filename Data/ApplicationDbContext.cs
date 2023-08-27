using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project3.Models;

namespace Project3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<CustomUser> CustomUsers { get; set; }
        public virtual DbSet<FAQ> Announcements { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Contest> Contests { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<FAQ> FAQs { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Register> Registers { get; set; }
        public virtual DbSet<Submission> Submissions { get; set; }
        public virtual DbSet<Tip> Tips { get; set; }



    }
}