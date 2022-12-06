using Microsoft.AspNetCore.Mvc;

namespace your_recipe.Controllers
{
    public class SimpleController : Controller
    {
        public IActionResult Index()
        {
            //show msg including current date
            ViewData["Message"] = "Today is " + DateTime.Today.ToString();

            //better code as iterator tell what the outcome should be
            return View("Index");
        }
    }
}
