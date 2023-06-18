using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Homies.Data.Models
{
    [Comment("Type of the event")]
    public class Type
    {
        [Comment("Primary Key")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the type")]
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public List<Event> Events { get; set; } = new List<Event>();
    }
}
