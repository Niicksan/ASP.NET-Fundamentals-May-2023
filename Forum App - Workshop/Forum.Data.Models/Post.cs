namespace Forum.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Forum.Common.Validations.EntityValidations.Post;

    public class Post
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(ContentMinLength)]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;
    }
}
