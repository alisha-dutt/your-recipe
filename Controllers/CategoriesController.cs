using Microsoft.AspNetCore.Mvc;

namespace YourRecipe.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
