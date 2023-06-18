using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data.Models
{
    [Comment("Events Participants")]
    public class EventsParticipants
    {
        [Comment("Event Helper Id")]
        public string HelperId { get; set; } = null!;

        [Comment("Helper")]
        [ForeignKey(nameof(HelperId))]
        public IdentityUser Helper { get; set; } = null!;

        [Comment("Event Id")]
        public int EventId { get; set; }

        [Comment("Event")]
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;
    }
}