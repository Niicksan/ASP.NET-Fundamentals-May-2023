using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Models.Ad;
using SoftUniBazar.Services.Iterfaces;

namespace SoftUniBazar.Controllers
{
    public class AdController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IAdService adService;

        public AdController(ICategoryService categoryService, IAdService adService)
        {
            this.categoryService = categoryService;
            this.adService = adService;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            IEnumerable<AllAdsViewModel> allevents = await adService.GetAllAdsAsync();

            return View(allevents);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add()
        {
            AdFormViewModel model = await categoryService.GetAllCategoriesModelAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AdFormViewModel model)
        {
            string userId = GetUserId();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await adService.CreateAdAsync(userId, model);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Add));
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = GetUserId();
            bool isAdOwner = await adService.IsAdOwner(userId, id);

            if (!isAdOwner)
            {
                return RedirectToAction(nameof(All));
            }

            var adForedit = await adService.GetAdForEditByIdAsync(id);

            if (adForedit == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(adForedit);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, AdFormViewModel model)
        {
            string userId = GetUserId();
            bool isAdOwner = await adService.IsAdOwner(userId, id);

            if (!isAdOwner)
            {
                return RedirectToAction(nameof(All));
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            try
            {
                await adService.EditAdAsync(id, model);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Edit));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(int id)
        {
            var isAdExists = await adService.IsAdExist(id);
            if (!isAdExists)
            {
                return RedirectToAction(nameof(All));
            }

            string userId = GetUserId();

            bool isAdOwner = await adService.IsAdOwner(userId, id);
            if (isAdOwner)
            {
                return RedirectToAction(nameof(All));
            }

            var IsAdAlreadyAddedToCart = await adService.IsAdAlreadyAddedToCart(userId, id);
            if (IsAdAlreadyAddedToCart)
            {
                return RedirectToAction(nameof(All));
            }

            await adService.AddAdToCart(userId, id);

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var isAdExists = await adService.IsAdExist(id);

            if (!isAdExists)
            {
                return RedirectToAction(nameof(All));
            }

            string userId = GetUserId();
            var IsAdAlreadyAddedToCart = await adService.IsAdAlreadyAddedToCart(userId, id);

            if (!IsAdAlreadyAddedToCart)
            {
                return RedirectToAction(nameof(All));
            }

            await adService.RemoveAdFromCart(userId, id);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public async Task<IActionResult> Cart()
        {
            string userId = GetUserId();

            var model = await adService.GetMyCartAsync(userId);

            return View(model);
        }
    }
}
