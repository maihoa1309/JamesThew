using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;
using Project3.DTO;
using Microsoft.Extensions.Primitives;

namespace Project3.Repository
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {
        Task<bool> CreateRecipe(FormAddRecipe request);
        Task<List<RecipeDetailDTO>> GetAllRecipes();
    }
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }

        public List<Recipe> GetLatestCreatedRecipes(int count)
        {
            // Sử dụng LINQ để truy vấn dữ liệu và lấy danh sách bản ghi được tạo gần nhất
            List<Recipe> latestRecipes = _context.Recipes.OrderByDescending(r => r.CreatedTime)
                                                       .Take(count)
                                                       .ToList();
            return latestRecipes;
        }
        public async Task<bool>  CreateRecipe(FormAddRecipe request) 
        {
            //var currentUser = _userManager.GetUserAsync(_contextAccessor.HttpContext.User).GetAwaiter().GetResult();

            var recipe = new Recipe();
            recipe.Title = request.Title;
            recipe.Description = request.Description;
            recipe.CookingTime = request.CookingTime;
            recipe.Servings = request.Servings;
            recipe.Instruction = request.Instruction;
            //recipe.UserId = currentUser.Id;
            recipe.Cuisines = request.Cuisines;
            recipe.IsFree = Convert.ToBoolean(request.IsFree);
            recipe.CreatedTime = DateTime.Now;
			
            List<IFormFile> imgs = new List<IFormFile>
            {
              request.Img1,
              request.Img2,
              request.Img3
            };
            foreach(var img in imgs) 
            {
                if (img != null)
                {
                //dat lai ten anh theo guid + phan extension
                string uploadImgName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                //upload file
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadImg", uploadImgName);
                //tao file moi vao duong dan upload copy img vao SreamUploadFile?
                using (var StreamUploadFile = new FileStream(savePath, FileMode.CreateNew))
                {
                    img.CopyTo(StreamUploadFile);
                }
                recipe.Img += ';'+uploadImgName;
                }
            }
			_dbSet.Add(recipe);
			_context.SaveChanges();
			var recipeId = recipe.Id;
			//upload anh 
			string[] ingres = new string[]
            {
                request.Ingredient,
                request.Ingredient1,
                request.Ingredient2,
                request.Ingredient3,
                request.Ingredient4,
                request.Ingredient5
            };
            string[] quantities = new string[]
            {
                request.Quantity,
                request.Quantity1,
                request.Quantity2,
                request.Quantity3,
                request.Quantity4,
                request.Quantity5
            };
            for(var i = 0; i < ingres.Length; i++)
            {
                if (ingres[i] != null)
                {
                    _context.RecipeDetail.Add(new RecipeDetail() { RecipeId = recipe.Id, IngredientId = int.Parse(ingres[i]), Unit = quantities[i] }) ;
                }
            }
			_context.SaveChanges();
			return true;
        }

        public Task<List<RecipeDetailDTO>> GetAllRecipes()
        {
            var allRecipes = from r in _context.Recipes
                             select r;

            throw new NotImplementedException();
        }
    }
}
