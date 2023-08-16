using SoftUniBazar.Models.Category;
using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Comman.EntityValidationConstants.Ad;

namespace SoftUniBazar.Models.Ad
{
    public class AdFormViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Name should be between 5 and 25 characters long")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "Description should be between 15 and 250 characters long")]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public List<CategorySelectViewModel> Categories { get; set; } = new List<CategorySelectViewModel>();
    }
}
