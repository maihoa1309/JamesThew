using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Repository;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIRegisterController : APIBaseController<Register>
    {
        public APIRegisterController(IBaseRepository<Register> repository) : base(repository)
        {
        }
    }
}
