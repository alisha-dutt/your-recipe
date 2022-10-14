using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using your_recipe.Models;
using YourRecipe.Models;

namespace YourRecipe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<YourRecipe.Models.Recipe> Recipes { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}