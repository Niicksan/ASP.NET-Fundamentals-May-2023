using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Introduction___Exercise.ViewModels.Chat
{
    public class MessageViewModel
    {
        [Required]
        public string Sender { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string MessageText { get; set; } = null!;
    }
}