using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models
{
    [Comment("Categoryies for the book")]
    public class Category
    {
        [Comment("Primery Key")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the Category")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public List<Book> Books { get; set; } = new List<Book>();
    }
}