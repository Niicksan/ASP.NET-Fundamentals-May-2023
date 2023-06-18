using Homies.Interfaces;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homies.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            IEnumerable<AllEventsViewModel> allevents = await eventService.GetAllEventsAsync();

            return View(allevents);
        }

        [Authorize]
        public async Task<IActionResult> Joined()
        {
            var model = await eventService.GetMyJoinedEventsAsync(GetUserId());

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add()
        {
            AddEventViewModel model = await eventService.GetAllTypesModelAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await eventService.AddEventAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = GetUserId();
            EventViewModel? eventById = await eventService.GetEventByIdAsync(id);

            if (eventById?.OrganiserId != userId)
            {
                return RedirectToAction(nameof(All));
            }

            AddEventViewModel? eventForedit = await eventService.GetEventByIdForEditAsync(id);

            if (eventForedit == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(eventForedit);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, AddEventViewModel model)
        {
            string userId = GetUserId();
            EventViewModel? eventById = await eventService.GetEventByIdAsync(id);

            if (eventById?.OrganiserId != userId)
            {
                return RedirectToAction(nameof(All));
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await eventService.EditEventAsync(model, id);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                EventDetailsViewModel viewModel =
                    await this.eventService.GetEventDetailsByIdAsync(id);

                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction("All", "Event");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCollection(int id)
        {
            var eventToAdd = await eventService.GetEventByIdAsync(id);

            if (eventToAdd == null)
            {
                return RedirectToAction(nameof(All));
            }

            var userId = GetUserId();

            await eventService.AddEventToCollectionAsync(userId, eventToAdd);

            return RedirectToAction(nameof(Joined));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            var eventToRemove = await eventService.GetEventByIdAsync(id);

            if (eventToRemove == null)
            {
                return RedirectToAction(nameof(All));
            }

            var userId = GetUserId();

            await eventService.RemoveEventFromCollectionAsync(userId, eventToRemove);

            return RedirectToAction(nameof(All));
        }
    }
}
