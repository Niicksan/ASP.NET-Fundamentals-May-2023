using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftUniBazar.Data.Models
{
    public class AdBuyer
    {
        [Comment("Ad Buyer Id")]
        public string BuyerId { get; set; } = null!;

        [Comment("Buyer")]
        [ForeignKey(nameof(BuyerId))]
        public IdentityUser Buyer { get; set; } = null!;

        [Comment("Ad Id")]
        public int AdId { get; set; }

        [Comment("Ad")]
        [ForeignKey(nameof(AdId))]
        public Ad Ad { get; set; } = null!;
    }
}
