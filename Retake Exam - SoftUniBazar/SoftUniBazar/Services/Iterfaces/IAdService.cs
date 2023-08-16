using SoftUniBazar.Models.Ad;

namespace SoftUniBazar.Services.Iterfaces
{
    public interface IAdService
    {
        Task<bool> IsAdExist(int adId);

        Task<bool> IsAdOwner(string userId, int adId);

        Task<bool> IsAdAlreadyAddedToCart(string userId, int adId);

        Task<IEnumerable<AllAdsViewModel>> GetAllAdsAsync();

        Task CreateAdAsync(string userId, AdFormViewModel model);

        Task<AdFormViewModel?> GetAdForEditByIdAsync(int adId);

        Task EditAdAsync(int adId, AdFormViewModel model);

        Task AddAdToCart(string userId, int adId);

        Task RemoveAdFromCart(string userId, int adId);

        Task<IEnumerable<AllAdsViewModel>> GetMyCartAsync(string userId);
    }
}
