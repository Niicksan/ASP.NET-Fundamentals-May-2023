using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Comman.EntityValidationConstants.Category;

namespace SoftUniBazar.Data.Models
{
    [Comment("Category of Ad")]
    public class Category
    {
        public Category()
        {
            this.Ads = new HashSet<Ad>();
        }

        [Comment("Primery key")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the Category")]
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Ad> Ads { get; set; }
    }
}
