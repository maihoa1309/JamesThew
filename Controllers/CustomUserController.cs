using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Project3.Data;
using Project3.DTO;
using Project3.Models;
using Project3.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class CustomUserController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly IUserRepository _repository;


		public CustomUserController(IUserRepository repository, ApplicationDbContext context)
		{
			_repository = repository;
			_context = context;
		}

        public async Task<UserDTO> GetUser(string keyword, int index, int size)
        {
            var result = await _repository.GetAllAsync(keyword, index, size);
            return result;
        }
        public async Task<bool> DeleteUser( string id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }
        public async Task<bool> SaveUser([FromBody] CustomUser req)
        {
            await _repository.UpdateUserAsync(req);
            return true;
        }
    }
}