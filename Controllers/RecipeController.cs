﻿using MessagePack.Formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project3.DTO;
using Project3.Models;
using Project3.Repository;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class RecipeController : BaseController<Recipe>
    {
        private readonly IBaseRepository<Recipe> _repository;
		private readonly IRecipeRepository _recipeRepository;
		public RecipeController(IBaseRepository<Recipe> repository,IRecipeRepository recipeRepository) : base(repository)
        {
            _recipeRepository= recipeRepository;
        }
        public IActionResult CreateRecipe ( FormAddRecipe request)
        {
			
            _recipeRepository.CreateRecipe(request);
            return Json(request);
        }
    }
}