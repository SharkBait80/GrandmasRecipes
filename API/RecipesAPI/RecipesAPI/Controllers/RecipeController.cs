using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DataLayer;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipesAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Recipe")]
    public class RecipeController : Controller
    {

        // GET api/recipe
        // Returns a random recipe
        [HttpGet()]
        public object Get()
        {
            RecipesDbContext dbContext = new RecipesDbContext();

            var largestId = dbContext.Recipes.OrderByDescending(r => r.Id).Select(x => (long?)x.Id).FirstOrDefault();

            if (largestId == null)
                return null;
            else
            {
                Random rnd = new Random(DateTime.Now.Millisecond);

                Recipe recipe = null;

                while (recipe == null)
                {
                    var randomId = rnd.Next(1, Convert.ToInt32(largestId.Value) + 1);
                    recipe = dbContext.Recipes.FirstOrDefault(r => r.Id == randomId);
                }

                return new
                {
                    Id = recipe.Id,
                    RecipeText = recipe.RecipeText.Split("\n", StringSplitOptions.RemoveEmptyEntries),
                    ImagePath = recipe.ImagePath,
                    Name = recipe.Name
                };
            }
        }
    }
}