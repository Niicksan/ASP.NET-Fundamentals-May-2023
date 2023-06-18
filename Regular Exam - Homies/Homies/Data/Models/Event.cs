using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data.Models
{
    [Comment("Event for participation")]
    public class Event
    {
        [Comment("primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the event")]
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Comment("Description of the event")]
        [Required]
        [MinLength(15)]
        [MaxLength(150)]
        public string Description { get; set; } = null!;

        [Comment("Type Id of the event")]
        [Required]
        public int TypeId { get; set; }

        [Comment("Type of the event")]
        [ForeignKey(nameof(TypeId))]
        public Type Type { get; set; } = null!;

        [Comment("Organiser Id")]
        [Required]
        public string OrganiserId { get; set; } = null!;

        [Comment("Organiser")]
        [ForeignKey(nameof(OrganiserId))]
        public IdentityUser Organiser { get; set; } = null!;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public DateTime CreatedOn { get; set; }

        public List<EventsParticipants> EventsParticipants { get; set; } = new List<EventsParticipants>();
    }
}