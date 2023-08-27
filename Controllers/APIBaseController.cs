using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Repository;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController<T> : ControllerBase where T : Base
    {
        private readonly IBaseRepository<T> _repository;

        public APIBaseController(IBaseRepository<T> repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> Create(T entity)
        {
            var result = await _repository.CreateAsync(entity);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("ERROR!");
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(T entity)
        {
            var result = await _repository.UpdateAsync(entity);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("ERROR!");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(T entity)
        {
            var result = await _repository.DeleteAsync(entity);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("ERROR!");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAllAsync();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("ERROR!");
            }
        }
    }
}
