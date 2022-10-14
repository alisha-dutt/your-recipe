using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourRecipe.Models;
using your_recipe.Models;
using YourRecipe.Data;

namespace YourRecipe.Controllers
{
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MenuController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        public IActionResult Category(string Name)
        {
            // for null values
            if(Name == null)
            {
                return RedirectToAction("Index");
            }
            // getting the name param from the url & store in viewdata to display it
            ViewData["Category"]= Name;
            
            // use recipe model to create list of dishes for display
            var recipes = new List<Recipe>();
            for(var i =1; i<6; i++)
            {
                recipes.Add(new Recipe { RecipeId = i, Name= "Recipe" +i.ToString(),Creator= "User",  Steps = "lorem ipsum dolor"});
            }

            return View(recipes);
        }

        public async Task<IActionResult> CategoryDetails(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = _context.Recipes.Where(m => m.CategoryId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(await recipe.ToListAsync());
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        // POST: Menu/AddCategory
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory([Bind("CategoryId,Name,CategoryType")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        // GET: Menu/CategoryDelete/5
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if(_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool CategoryExists(int id)
        {
          return _context.Categories.Any(e => e.CategoryId == id);
        }

        // GET: Menu/EditCategory/5
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Menu/EditCategory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, [Bind("CategoryId,Name,CategoryType")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

    }
}
