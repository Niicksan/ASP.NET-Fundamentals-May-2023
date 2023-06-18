using Homies.Data;
using Homies.Data.Models;
using Homies.Interfaces;
using Homies.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using Event = Homies.Data.Models.Event;

namespace Homies.Services
{
    public class EventService : IEventService
    {
        private readonly HomiesDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public EventService(HomiesDbContext dbContex, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContex;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<AllEventsViewModel>> GetAllEventsAsync()
        {
            return await dbContext
               .Events
               .Select(e => new AllEventsViewModel
               {
                   Id = e.Id,
                   Name = e.Name,
                   Type = e.Type.Name,
                   Start = e.Start.ToString("dd/MM/yyy H:mm"),
                   Organiser = e.Organiser.UserName
               }).ToArrayAsync();
        }

        public async Task<IEnumerable<AllEventsViewModel>> GetMyJoinedEventsAsync(string userId)
        {
            return await dbContext.EventsParticipants
               .Where(ep => ep.HelperId == userId)
               .Select(e => new AllEventsViewModel
               {
                   Id = e.Event.Id,
                   Name = e.Event.Name,
                   Type = e.Event.Type.Name,
                   Start = e.Event.Start.ToString("dd/MM/yyy H:mm"),
                   Organiser = e.Event.Organiser.UserName
               }).ToListAsync();
        }

        public async Task AddEventAsync(AddEventViewModel model)
        {
            var newEvent = new Event
            {
                Name = model.Name,
                Description = model.Description,
                TypeId = model.TypeId,
                OrganiserId = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                CreatedOn = DateTime.UtcNow,
            };

            if (model.Start.ToString() != "")
            {
                var date = model.Start.ToString("yyyy-MM-dd H:mm");
                newEvent.Start = DateTime.ParseExact(date, "yyyy-MM-dd H:mm", CultureInfo.InvariantCulture);
            }

            if (model.End.ToString() != "")
            {
                var date = model.End.ToString("yyyy-MM-dd H:mm");
                newEvent.End = DateTime.ParseExact(date, "yyyy-MM-dd H:mm", CultureInfo.InvariantCulture);
            }

            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditEventAsync(AddEventViewModel model, int id)
        {
            Event eventForEdit = await dbContext.Events.FindAsync(id);

            if (eventForEdit != null)
            {
                eventForEdit.Name = model.Name;
                eventForEdit.Description = model.Description;
                eventForEdit.TypeId = model.TypeId;

                if (model.Start.ToString() != "")
                {
                    var date = model.Start.ToString("yyyy-MM-dd H:mm");
                    eventForEdit.Start = DateTime.ParseExact(date, "yyyy-MM-dd H:mm", CultureInfo.InvariantCulture);
                }

                if (model.End.ToString() != "")
                {
                    var date = model.End.ToString("yyyy-MM-dd H:mm");
                    eventForEdit.End = DateTime.ParseExact(date, "yyyy-MM-dd H:mm", CultureInfo.InvariantCulture);
                }

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<EventViewModel?> GetEventByIdAsync(int id)
        {
            return await dbContext.Events
                .Where(e => e.Id == id)
                .Select(e => new EventViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Start = e.Start,
                    End = e.End,
                    Type = e.Type.Name,
                    Organiser = e.Organiser.UserName,
                    OrganiserId = e.Organiser.Id
                }).FirstOrDefaultAsync();
        }

        public async Task<AddEventViewModel?> GetEventByIdForEditAsync(int id)
        {
            var types = await dbContext.Types
                .Select(t => new TypeViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToListAsync();

            return await dbContext.Events
                .Where(e => e.Id == id)
                .Select(e => new AddEventViewModel
                {
                    Name = e.Name,
                    Description = e.Description,
                    TypeId = e.TypeId,
                    Start = e.Start,
                    End = e.End,
                    Types = types
                }).FirstOrDefaultAsync();
        }

        public async Task<EventDetailsViewModel> GetEventDetailsByIdAsync(int id)
        {
            EventDetailsViewModel viewModel = await this.dbContext
               .Events
               .Select(e => new EventDetailsViewModel()
               {
                   Id = e.Id,
                   Name = e.Name,
                   Description = e.Description,
                   Organiser = e.Organiser.UserName,
                   Start = e.Start.ToString("dd/MM/yyy H:mm"),
                   End = e.End.ToString("dd/MM/yyy H:mm"),
                   CreatedOn = e.CreatedOn.ToString("dd/MM/yyy H:mm"),
                   Type = e.Type.Name
               })
               .FirstAsync(e => e.Id == id);

            return viewModel;
        }

        public async Task<AddEventViewModel> GetAllTypesModelAsync()
        {
            var types = await dbContext.Types
               .Select(c => new TypeViewModel
               {
                   Id = c.Id,
                   Name = c.Name
               }).ToListAsync();

            var model = new AddEventViewModel
            {
                Types = types
            };

            return model;
        }

        public async Task AddEventToCollectionAsync(string userId, EventViewModel eventToAdd)
        {
            bool alreadyAdded = await dbContext.EventsParticipants
               .AnyAsync(ep => ep.HelperId == userId && ep.EventId == eventToAdd.Id);

            if (alreadyAdded == false)
            {
                var eventParticipant = new EventsParticipants
                {
                    HelperId = userId,
                    EventId = eventToAdd.Id
                };

                await dbContext.EventsParticipants.AddAsync(eventParticipant);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveEventFromCollectionAsync(string userId, EventViewModel eventToRemove)
        {
            var eventParticipant = await dbContext.EventsParticipants
                     .FirstOrDefaultAsync(ep => ep.HelperId == userId && ep.EventId == eventToRemove.Id);

            if (eventParticipant != null)
            {
                dbContext.EventsParticipants.Remove(eventParticipant);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
