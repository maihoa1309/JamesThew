using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;
using Project3.DTO;
using Microsoft.Extensions.Primitives;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Linq;

namespace Project3.Repository
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {

        Task<Recipe> SaveRecipeAsync(FormAddRecipe request);
        Task<List<RecipeDetailDTO>> GetAllRecipesAsync();
        Task<List<RecipeDetailDTO>> GetLatestCreatedRecipes(int count);
        Task<RecipeDetailDTO> GetRecipeByIdAsync (int id);
        Task<List<RecipeDetailDTO>> GetRecipeByUserAsync();
        Task<List<RecipeDetailDTO>> GetByNameAsync(string keyword, int index = 1, int size = 10);
        Task<List<CategoryDetail>> GetRandomCategories(int count);
    
    }
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public RecipeRepository(ApplicationDbContext dbContext, UserManager<CustomUser> userManager, IHttpContextAccessor httpContext, IWebHostEnvironment hostingEnviroment) : base(dbContext, userManager, httpContext) {
            _hostingEnvironment = hostingEnviroment;
        }

        public async Task<List<RecipeDetailDTO>> GetLatestCreatedRecipes(int count)
        {
            // Sử dụng LINQ để truy vấn dữ liệu và lấy danh sách bản ghi được tạo gần nhất

            var query = await _dbSet.OrderByDescending(r => r.CreatedTime)
                                                    .Take(count).ToListAsync();


            var categories = (from r in _context.Recipes
                          join c in _context.Categories on r.CategoryId equals c.Id
                          select new CategoryDetail
                          {
                            CategoryId = c.Id,
                            CategoryName = c.Name
                          }).ToList();
            var result = (from q in query
                         join c in categories on q.CategoryId equals c.CategoryId
                         group c by q into grouped
                         select new RecipeDetailDTO
                         {
                            Recipe = grouped.Key,
                            Category = grouped.FirstOrDefault()
                         }).ToList();
            return result;
        }

        public async Task<List<CategoryDetail>> GetRandomCategories(int count)
        {
            var randomCategories = await _context.Categories
                .OrderBy(x => Guid.NewGuid()) // Sắp xếp danh mục ngẫu nhiên
                .Take(count)
                .Select(c => new CategoryDetail
                {
                    CategoryId = c.Id,
                    CategoryName = c.Name
                })
                .ToListAsync();

            return randomCategories;
        }


        public async Task<List<RecipeDetailDTO>> GetAllRecipesAsync()
        {
            List<Recipe> allRecipes = await (from r in _context.Recipes
                                    select r).ToListAsync();
            var allIngredients = await (from rd in _context.RecipeDetail
                                join i in _context.Ingredients on rd.IngredientId equals i.Id
                                select new IngredientDetail
                                {
                                    RecipeId = rd.RecipeId,
                                    IngredientId = rd.IngredientId,
                                    Quantity = rd.Quantity,
                                    Name = i.Name 
                                }).ToListAsync();
            var query = (from r in allRecipes
                         join i in allIngredients on r.Id equals i.RecipeId
                         join u in _userManager.Users on r.UserId equals u.UserName
                         group i by new { r.Id, r.Title, u.UserName}  into grouped
                         select new RecipeDetailDTO
                         {
                             RecipeId = grouped.Key.Id,
                             RecipeName = grouped.Key.Title,
                             Ingredients = grouped.ToList(),
                             Username = grouped.Key.UserName
                         }).ToList();

             return query;
        }
        public async Task<RecipeDetailDTO> GetRecipeByIdAsync(int id)
        {
			
            RecipeDetailDTO result = new RecipeDetailDTO();

            var recipe = (from r in _context.Recipes
                          where r.Id == id select r).FirstOrDefault();
            var allIngredients = await (from rd in _context.RecipeDetail
                                        join i in _context.Ingredients on rd.IngredientId equals i.Id
                                        where rd.RecipeId == id
                                        select new IngredientDetail
                                        {
                                            RecipeId = rd.RecipeId,
                                            IngredientId = rd.IngredientId,
                                            Quantity = rd.Quantity,
                                            Name = i.Name
                                        }).ToListAsync();
            var category = (from c in _context.Categories
                            join r in _context.Recipes on c.Id equals r.CategoryId
                            where r.Id == id
                            select new CategoryDetail
                            {
                                CategoryId = c.Id,
                                CategoryName = c.Name
                            }).FirstOrDefault();
                         
            var user = (from r in _context.Recipes 
                        join u in _userManager.Users on r.UserId equals u.Id
                        where r.Id == id
                        select u).FirstOrDefault();
            result.User = user;
            result.Recipe= recipe;
            result.Ingredients= allIngredients;
            result.Category = category;
            return result;

        }


        public Task<List<RecipeDetailDTO>> GetRecipeByUserAsync()
        {
            var currentUser = _userManager.GetUserAsync(_contextAccessor.HttpContext.User).GetAwaiter().GetResult();
            throw new NotImplementedException();
        }

        public async Task<Recipe> SaveRecipeAsync(FormAddRecipe request)
        {
            
            if(request != null)
            {
                var Recipe = new Recipe();
                if(request.id > 0)
                {
                    Recipe = _dbSet.Find(request.id);
                }

                if(Recipe != null)
                {
                    //Up anh len truoc
                    if (request.imgs != null)
                    {
                        var imgs = UploadImageFromBase64(request.imgs.ToList());
                        Recipe.Img = string.Join(',', imgs);
                    }
                    Recipe.Title = request.title;
                    Recipe.Description = request.description;
                    Recipe.CookingTime = request.cookingTime;
                    Recipe.Servings = request.servings;
                    Recipe.Instruction = request.instruction;
                    //Recipe.UserId = currentUser.Id;
                    Recipe.Cuisines = request.cuisines;
                    Recipe.IsFree = Convert.ToBoolean(request.isFree);
                    Recipe.CreatedTime = DateTime.Now;
                    Recipe.CategoryId = int.Parse(request.category);
                    Recipe.IsDeleted = false;
                    if(Recipe.Id > 0)
                    {
                        _context.Recipes.Update(Recipe);
                    }
                    if(Recipe.Id <= 0)
                    {
                        _context.Recipes.Add(Recipe);

                    }                                        

                    await _context.SaveChangesAsync();
                    //Lay ra duoc cai RecipeId
                    var RecipeId = Recipe.Id;
                    //Lam viec voi bang RecipeDetails..
                    //Lay ra toan bo detail
                    var RecipeDetails = _context.RecipeDetail.Where(r => r.RecipeId == RecipeId);
                    //Xoa toan bo detail cu
                    if (RecipeDetails != null)
                    {
                        foreach(var item in RecipeDetails)
                        {
                            _context.RecipeDetail.RemoveRange(RecipeDetails);
                        }
                    }
                    //Them moi lai detail moi
                    foreach(var item in request.ingredients)
                    {
                        var detail = new RecipeDetail();
                        detail.RecipeId= RecipeId;
                        detail.IngredientId = int.Parse(item.ingredientId);
                        detail.Quantity = item.quantity;
                        _context.RecipeDetail.Add(detail);
                    }

                    await _context.SaveChangesAsync();
                }
                return Recipe;

            }
            return null;
        }

        private List<string> UploadImageFromBase64(List<string> imgsBase64)
        {
            var result = new List<string>();
            // Lấy đường dẫn tới thư mục wwwroot/UploadImg
            var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "UploadImg");

            // Tạo thư mục UploadImg nếu chưa tồn tại
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            foreach(var item in imgsBase64)
            {
                if (item.StartsWith("/UploadImg"))
                {
                    result.Add(item.TrimStart('/'));
                }
                else
                {
                    // Tạo tên file duy nhất bằng guid
                    var fileName = $"{Guid.NewGuid()}.jpg";
                    // Giải mã base64 thành mảng byte
                    var imageBytes = Convert.FromBase64String(item.Substring(item.IndexOf(',') + 1));
                    // Tạo đường dẫn tới file ảnh
                    var imagePath = Path.Combine(uploadPath, fileName);
                    // Lưu file ảnh vào thư mục UploadImg
                    System.IO.File.WriteAllBytes(imagePath, imageBytes);
                    // Trả về đường dẫn của file ảnh đã lưu
                    var imageUrl = Path.Combine("UploadImg/", fileName);
                    result.Add(imageUrl);
                }
            }
            return result;
        }

        public async Task<List<RecipeDetailDTO>> GetByNameAsync(string keyword, int index , int size)
        {
            List<Recipe> allRecipes = await _context.Recipes.ToListAsync();
            if (!string.IsNullOrEmpty(keyword))
            {
                allRecipes = allRecipes.Where(r => r.Title.ToLower().Contains(keyword.ToLower())).ToList();
            }

            var query = (from r in allRecipes
                         join u in _userManager.Users on r.UserId equals u.Id
                         select new RecipeDetailDTO
                         {
                             Recipe = r,
                             User = u,
                         }).ToList();
            var total = query.Count();
            query = query.OrderByDescending(r => r.RecipeId).Skip((index - 1) * size).Take(size).ToList();
            query[0].TotalRow = total;
            return query;

        }

	
	}
}

