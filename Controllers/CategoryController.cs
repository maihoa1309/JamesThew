using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Repository;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class CategoryController : BaseController<Category>
    {
        public CategoryController(IBaseRepository<Category> repository) : base(repository)
        {
        }

        // Các phương thức cụ thể cho CategoryController (nếu cần)

        // Ví dụ phương thức tùy chỉnh

    }
}