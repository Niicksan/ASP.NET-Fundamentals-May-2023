using System.ComponentModel.DataAnnotations;

namespace Homies.Models
{
    public class AddEventViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(150, MinimumLength = 15)]
        public string Description { get; set; } = null!;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        [Range(1, int.MaxValue)]
        public int TypeId { get; set; }

        public List<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();
    }
}
