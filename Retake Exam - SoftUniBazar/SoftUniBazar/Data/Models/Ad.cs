using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static SoftUniBazar.Comman.EntityValidationConstants.Ad;

namespace SoftUniBazar.Data.Models
{
    [Comment("Ad")]
    public class Ad
    {
        public Ad()
        {
            this.AdBuyers = new HashSet<AdBuyer>();
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the Ad")]
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Comment("Description of the Ad")]
        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Comment("Price of the Ad")]
        [Required]
        public decimal Price { get; set; }

        [Comment("Image Url of the Ad")]
        [Required]
        public string ImageUrl { get; set; }

        [Comment("Owner Id")]
        [Required]
        public string OwnerId { get; set; } = null!;

        [Comment("Owner")]
        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;

        [Comment("Category Id of the Ad")]
        [Required]
        public int CategoryId { get; set; }

        [Comment("Category of the Ad")]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Comment("Date of creation")]
        [Required]
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<AdBuyer> AdBuyers { get; set; }
    }
}
