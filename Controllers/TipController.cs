using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Repository;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class TipController : BaseController<Tip>
    {
        public TipController(IBaseRepository<Tip> repository) : base(repository)
        {
        }
    }
}