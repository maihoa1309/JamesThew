using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Repository;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class CategoryController : BaseController<Category>
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IBaseRepository<Category> repository, ICategoryRepository categoryRepository) : base(repository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> SaveCategory([FromBody] Category category)
        {
            await _categoryRepository.SaveCategoryAsync(category);
            return Json(category);
        }

    }
}