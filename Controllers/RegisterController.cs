using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Repository;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class RegisterController : BaseController<Register>
    {
        public RegisterController(IBaseRepository<Register> repository) : base(repository)
        {
        }
    }
}