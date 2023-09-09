using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;
using Project3.DTO;
using Microsoft.Extensions.Primitives;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Project3.Repository
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {
        Task<bool> CreateRecipe(FormAddRecipe request);
        Task<List<RecipeDetailDTO>> GetAllRecipes();
        Task<List<Recipe>> GetLatestCreatedRecipes(int count);
        Task<RecipeDetailDTO> GetRecipeById (int id);
        Task<List<RecipeDetailDTO>> GetRecipeByUser();
    }
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext) : base(dbContext, userManager, httpContext) { }

        public async Task<List<Recipe>> GetLatestCreatedRecipes(int count)
        {
            // Sử dụng LINQ để truy vấn dữ liệu và lấy danh sách bản ghi được tạo gần nhất

            var query = await _dbSet.OrderByDescending(r => r.CreatedTime)
                                                    .Take(count).ToListAsync();
            //var query2 = await _dbSet.OrderByDescending(r => r.CreatedTime)
            //                                        .FirstOrDefaultAsync();

            return query;
        }
        public async Task<bool> CreateRecipe(FormAddRecipe request)
        {
            var currentUser = _userManager.GetUserAsync(_contextAccessor.HttpContext.User).GetAwaiter().GetResult();

            var recipe = new Recipe();
            recipe.Title = request.Title;
            recipe.Description = request.Description;
            recipe.CookingTime = request.CookingTime;
            recipe.Servings = request.Servings;
            recipe.Instruction = request.Instruction;
            recipe.UserId = currentUser.Id;
            recipe.Cuisines = request.Cuisines;
            recipe.IsFree = Convert.ToBoolean(request.IsFree);
            recipe.CreatedTime = DateTime.Now;

            List<IFormFile> imgs = new List<IFormFile>
            {
                request.Img1,
                request.Img2,
                request.Img3
            };
            foreach (var img in imgs)
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
                    recipe.Img += ';' + uploadImgName;
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
            for (var i = 0; i < ingres.Length; i++)
            {
                if (ingres[i] != null)
                {
                    _context.RecipeDetail.Add(new RecipeDetail() { RecipeId = recipe.Id, IngredientId = int.Parse(ingres[i]), Unit = quantities[i] });
                }
            }
            _context.SaveChanges();
            return true;
        }

        public async Task<List<RecipeDetailDTO>> GetAllRecipes()
        {
            List<Recipe> allRecipes = await (from r in _context.Recipes
                                    select r).ToListAsync();
            var allIngredients =await  (from rd in _context.RecipeDetail
                                join i in _context.Ingredients on rd.IngredientId equals i.Id
                                group rd by new {rd.RecipeId, rd.IngredientId} into grouped
                                select new IngredientDetail
                                {
                                    RecipeId = grouped.Key.RecipeId,
                                    IngredientId = grouped.Key.IngredientId,
                                    Quantity = grouped.Select(rd => rd.Quantity).FirstOrDefault()
                                }).ToListAsync();
            var query = (from r in allRecipes
                         join i in allIngredients on r.Id equals i.RecipeId
                         join u in _userManager.Users on r.UserId equals u.Id
                         group new { i, u } by r into grouped
                         select new RecipeDetailDTO
                         {
                             Recipe = grouped.Key,
                             Ingredients = grouped.Select(x => x.i).ToList(),
                             User = grouped.Select(x=>x.u).FirstOrDefault()
                         }).ToList();
         
             return query;
        }

        public async Task<RecipeDetailDTO> GetRecipeById(int id)
        {
            RecipeDetailDTO result = new RecipeDetailDTO();
            var recipe = (from r in _context.Recipes
                         where r.Id == id select r).FirstOrDefault();
            var allIngredients = await(from rd in _context.RecipeDetail
                                       join i in _context.Ingredients on rd.IngredientId equals i.Id
                                       where rd.RecipeId == id
                                       group rd by new { rd.RecipeId, rd.IngredientId } into grouped
                                       select new IngredientDetail
                                       {
                                           RecipeId = grouped.Key.RecipeId,
                                           IngredientId = grouped.Key.IngredientId,
                                           Quantity = grouped.Select(rd => rd.Quantity).FirstOrDefault()
                                       }).ToListAsync();
            var user = (from r in _context.Recipes 
                        join u in _userManager.Users on r.UserId equals u.Id
                        where r.Id == id
                        select u).FirstOrDefault();
            result.User = user;
            result.Recipe= recipe;
            result.Ingredients= allIngredients;

            return result;
        }

        public Task<List<RecipeDetailDTO>> GetRecipeByUser()
        {
            var currentUser = _userManager.GetUserAsync(_contextAccessor.HttpContext.User).GetAwaiter().GetResult();


            throw new NotImplementedException();
        }
    }
}

