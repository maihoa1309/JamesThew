using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Repository;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class AnnouncementController : BaseController<Announcement>
    {
        public AnnouncementController(IBaseRepository<Announcement> repository) : base(repository)
        {
        }

        // Các phương thức cụ thể cho AnnouncementController (nếu cần)

        // Ví dụ phương thức tùy chỉnh
        
    }
}