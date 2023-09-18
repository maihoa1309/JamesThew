
﻿using Microsoft.AspNetCore.Mvc;
using Project3.Models;
using Project3.Repository;

namespace Project3.Controllers
{
	public class IngredientController : BaseController<Ingredient>
	{
		private readonly IBaseRepository<Ingredient> _repository;
		private readonly IIngerdientRepository _ingredientRepository;

		public IngredientController(IBaseRepository<Ingredient> repository, IIngerdientRepository ingredientRepository) : base(repository)
		{

			_ingredientRepository = ingredientRepository;
		}
		public IActionResult CreateIngredient(Ingredient request)
		{
			_ingredientRepository.CreateAsync(request).Wait();
			return new JsonResult(request);
		}
		public IActionResult DeleteIngredient (Ingredient entity)
		{
			_ingredientRepository.DeleteAsync(entity).Wait();
			return RedirectToAction("Ingredients", "Admin");
			//return Json(entity);
		}
		public async Task<List<Ingredient>> GetByName(string keyword, int index, int size)
		{
			var result = await _ingredientRepository.GetByNameAsync(keyword, index, size);
			return result;
		}
	}
}

