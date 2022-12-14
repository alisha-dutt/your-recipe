using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using your_recipe.Models;
using YourRecipe.Data;
using YourRecipe.Models;

namespace your_recipe.Controllers
{
    
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //specifying view for unit testing
            return View("Index", await _context.Recipes.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecipe([Bind("RecipeId,Name,Creator,Steps,Photo,CategoryId")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Recipes/EditRecipe/2
        [Authorize]
        public async Task<IActionResult> EditRecipe(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return View("404");
            }
          
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return View("404");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View("EditRecipe", recipe);
        }


        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeId == id);
        }

        // POST: Recipes/EditCategory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> EditRecipe(int id, [Bind("RecipeId,Name,Creator,Steps,Photo,CategoryId")] Recipe recipe)
        {
            if (id != recipe.RecipeId)
            {
                return View("404");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.RecipeId))
                    {
                        return View("404");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/RecipeDelete/5
       
        public async Task<IActionResult> DeleteRecipe(int? id)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Recipes'  is null.");
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Recipes/RecipeDetails/5
        
        public async Task<IActionResult> RecipeDetails(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return View("404");
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipe == null)
            {
                return View("404");
            }

            return View(recipe);
        }

    }
}
