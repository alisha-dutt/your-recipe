using Microsoft.AspNetCore.Mvc;
using your_recipe.Controllers;

namespace yourRecipeTests
{
    //this file contains unit test for the methods in YourRecipe web app.
    [TestClass]
    public class SimpleControllerTest
    {
        [TestMethod]
        public void IndexReturnsSomething()
        {
            //arrange - set up conditions to try the Index method
            var controller = new SimpleController();

            //act - execute the Index method
            var result = controller.Index();

            //assert - did the method return something and not a null response?
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void IndexLoadsIndexView()
        {
            //arrange - set up conditions to try the Index method
            var controller = new SimpleController();

            //act - execute the Index method
            var result = (ViewResult)controller.Index();

            //assert - did the method return index view and not a null response?
            Assert.AreEqual("Index", result.ViewName);

        }
        [TestMethod]
        public void IndexViewDataShowsCurrentDate()
        {
            //arrange - set up conditions to try the Index method
            var controller = new SimpleController();

            //act - execute the Index method
            var result = (ViewResult)controller.Index();

            //assert - did the method return index view and not a null response?
            Assert.AreEqual("Today is " + DateTime.Today.ToString(), result.ViewData["Message"].ToString());

        }
    }
}