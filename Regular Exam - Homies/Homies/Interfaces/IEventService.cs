using Homies.Models;

namespace Homies.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<AllEventsViewModel>> GetAllEventsAsync();

        Task<IEnumerable<AllEventsViewModel>> GetMyJoinedEventsAsync(string userId);

        Task AddEventAsync(AddEventViewModel model);

        Task EditEventAsync(AddEventViewModel model, int id);

        Task<EventViewModel?> GetEventByIdAsync(int id);

        Task<AddEventViewModel?> GetEventByIdForEditAsync(int id);

        Task<EventDetailsViewModel> GetEventDetailsByIdAsync(int id);

        Task<AddEventViewModel> GetAllTypesModelAsync();

        Task AddEventToCollectionAsync(string userId, EventViewModel eventToAdd);

        Task RemoveEventFromCollectionAsync(string userId, EventViewModel eventToRemove);
    }
}
