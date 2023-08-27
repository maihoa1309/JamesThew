﻿using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {

    }
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
