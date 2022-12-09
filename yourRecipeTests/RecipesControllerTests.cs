using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using your_recipe.Controllers;
using your_recipe.Models;
using YourRecipe.Controllers;
using YourRecipe.Data;
using YourRecipe.Models;

namespace yourRecipeTests
{
    [TestClass]
    public class RecipesControllerTests
    {
        private ApplicationDbContext _context;
        private RecipesController controller;

        [TestInitialize]    
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            //add mock data
            var category = new Category
            {
                CategoryId = 1000,
                Name = "Test Category"
            };
            _context.Add(category);
            for (var i = 1; i <= 3; i++)
            {
                var recipe = new Recipe
                {
                    CategoryId = 1000,
                    Name = "Recipe" + i.ToString(),
                    Creator = "Creator" + i.ToString(),
                    Steps = "Steps" + i.ToString(),
                    Photo = "photo" + i.ToString(),
                    Category = category
                };
                _context.Add(recipe);
            }
            _context.SaveChanges();
            controller = new RecipesController(_context);

        }
        //Test 1: To check if  Recipe Index returns the view

        [TestMethod]
        public void IndexloadsIndexView()
        {
            var result = (ViewResult)controller.Index().Result;
            Assert.AreEqual("Index", result.ViewName);
        }

        //Test 2: To check if recipe Index Loads the Recipes
        [TestMethod]
        public void IndexLoadsRecipes()
        {
            //act
            var result = (ViewResult)controller.Index().Result;
            List<Recipe> model = (List<Recipe>)result.Model;
            //assert
            CollectionAssert.AreEqual(_context.Recipes.ToList(), model);
        }

        // Test 3:  for EditRecipe page returning 404 page if ID not found
        [TestMethod]

        public void EditNoIdLoads404()
        {
            var result = (ViewResult)controller.EditRecipe(null).Result;
            //assert
            Assert.AreEqual("404", result.ViewName);
        }

        // Test 4: for EditRecipe page has an ID but no Recipes
        [TestMethod]
        public void EditNoRecipeLoads404()
        {
            // arrange - remove the recipe from the db 
            _context.Recipes = null;

            // act - pass ID
            var result = (ViewResult)controller.EditRecipe(1).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        //test 5: for EditRecipe Invalid Id returning 404 page
        [TestMethod]
        public void EditRecipeInvalidIdLoads404()
        {
           var result = (ViewResult)controller.EditRecipe(5).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        //test 6: passing Valid ID
        [TestMethod]
        public void EditValidIdLoadsRecipe()
        {
            // use valid id 1, 2, 3
            var result = (ViewResult)controller.EditRecipe(2).Result;
            var model = (Recipe)result.Model;

            // assert
            Assert.AreEqual(_context.Recipes.Find(2), model);
        }

        //test 7: passing Valid ID and returning the view
        [TestMethod]
        public void EditPagevalidIdLoadsValidView()
        {
            var result = (ViewResult)controller.EditRecipe(3).Result;
            // assert
            Assert.AreEqual("EditRecipe", result.ViewName);
        }

        //Test8: EditRecipe saves chnages 
        [TestMethod]

        public void EditSavesValidRecipe()
        {
            var Recipe = _context.Recipes.Find(1);
            Recipe.Name = "Cheese Chilli";

            var result = (RedirectToActionResult)controller.EditRecipe(1, Recipe).Result;

            Assert.AreEqual(Recipe, _context.Recipes.Find(1));
        }

        

    }
}
