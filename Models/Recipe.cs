using System.ComponentModel.DataAnnotations;
using your_recipe.Models;

namespace YourRecipe.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        [MinLength (5, ErrorMessage = "Recipe name is too short.")]
        [MaxLength (50, ErrorMessage ="Recipe name is too long")]
        [Required]
        public string? Name { get; set; }

        [MinLength(2, ErrorMessage = " Creator Name is too short.")]
        [MaxLength(50, ErrorMessage = "Creator name is too long")]
        [Required]
        public string? Creator { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = " Your recipe steps are too short")]
        public string? Steps { get; set; }

        public string? Photo { get; set; }

        //FK for category Table

        public int CategoryId { get; set; }

        //ref to parent model
        public Category? Category { get; set; }

    }
}
