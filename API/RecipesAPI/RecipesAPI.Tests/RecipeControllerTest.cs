using RecipesAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RecipesAPI.Tests
{
    public class RecipeControllerTest
    {
        [Fact]
        public async Task TestGet()
        {
            new RecipeController().Get();
        }
    }
}
