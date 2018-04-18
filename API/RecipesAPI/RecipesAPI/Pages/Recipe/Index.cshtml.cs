using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipesAPI.Pages.Recipe
{
    public class IndexModel : PageModel
    {
        RecipesDbContext dbContext = new RecipesDbContext();
        public  List<Entities.Recipe> RecipeList = null;
        public IndexModel()
        {
            RecipeList = dbContext.Recipes.OrderBy(r => r.Name).ToList();
        }
        public void OnGet()
        {

        }
    }
}