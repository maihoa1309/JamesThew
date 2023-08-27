using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IRegisterRepository : IBaseRepository<Register>
    {

    }
    public class RegisterRepository : BaseRepository<Register>, IRegisterRepository
    {
        public RegisterRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
