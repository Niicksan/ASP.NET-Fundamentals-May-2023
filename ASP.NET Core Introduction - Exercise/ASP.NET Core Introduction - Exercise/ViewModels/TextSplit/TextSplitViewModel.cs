using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Introduction___Exercise.ViewModels.TextSplit
{
    public class TextSplitViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Text must be a string between 2 and 30 characters long!")]
        public string TextToSplit { get; set; } = null!;

        public string? SplitText { get; set; }
    }
}