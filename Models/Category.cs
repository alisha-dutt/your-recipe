using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using YourRecipe.Models;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace your_recipe.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength (50)]
        public string? Name { get; set; }
        public string? CategoryType { get; set; }

        // ref ti child recipes
        public List<Recipe>? Recipes { get; set; }
    }
}
