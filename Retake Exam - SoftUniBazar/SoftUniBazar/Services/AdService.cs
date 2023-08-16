using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models.Ad;
using SoftUniBazar.Models.Category;
using SoftUniBazar.Services.Iterfaces;
using System.Globalization;

namespace SoftUniBazar.Services
{
    public class AdService : IAdService
    {
        private readonly BazarDbContext dbContext;

        public AdService(BazarDbContext dbContex)
        {
            this.dbContext = dbContex;
        }

        public async Task<bool> IsAdExist(int adId)
        {
            return await dbContext
                .Ads
                .Where(a => a.Id == adId)
                .AnyAsync();
        }

        public async Task<bool> IsAdOwner(string userId, int adId)
        {
            return await dbContext
                .Ads
                .Where(a => a.OwnerId == userId && a.Id == adId)
                .AnyAsync();
        }

        public async Task<bool> IsAdAlreadyAddedToCart(string userId, int adId)
        {
            return await dbContext
                .AdBuyers
                .Where(ab => ab.BuyerId == userId && ab.AdId == adId)
                .AnyAsync();
        }

        public async Task<IEnumerable<AllAdsViewModel>> GetAllAdsAsync()
        {
            return await dbContext
               .Ads
               .Select(a => new AllAdsViewModel
               {
                   Id = a.Id,
                   Name = a.Name,
                   ImageUrl = a.ImageUrl,
                   Description = a.Description,
                   Price = a.Price,
                   Category = a.Category.Name,
                   Owner = a.Owner.UserName,
                   CreatedOn = a.CreatedOn.ToString("dd-MM-yyy H:mm"),
               }).ToArrayAsync();
        }

        public async Task CreateAdAsync(string userId, AdFormViewModel model)
        {
            var date = DateTime.UtcNow.ToString("yyyy-MM-dd H:mm");

            Ad newAd = new Ad
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CategoryId = model.CategoryId,
                OwnerId = userId,
                CreatedOn = DateTime.ParseExact(date, "yyyy-MM-dd H:mm", CultureInfo.InvariantCulture)
            };

            await dbContext.Ads.AddAsync(newAd);
            await dbContext.SaveChangesAsync();
        }

        public async Task<AdFormViewModel?> GetAdForEditByIdAsync(int adId)
        {
            var categories = await dbContext
                .Categories
                .Select(c => new CategorySelectViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();

            return await dbContext
                .Ads
                .Where(a => a.Id == adId)
                .Select(a => new AdFormViewModel
                {
                    Name = a.Name,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    Price = a.Price,
                    CategoryId = a.CategoryId,
                    Categories = categories
                }).FirstOrDefaultAsync();
        }

        public async Task EditAdAsync(int adId, AdFormViewModel model)
        {
            Ad? adForEdit = await dbContext.Ads.FindAsync(adId);

            if (adForEdit != null)
            {
                adForEdit.Name = model.Name;
                adForEdit.Description = model.Description;
                adForEdit.ImageUrl = model.ImageUrl;
                adForEdit.Price = model.Price;
                adForEdit.CategoryId = model.CategoryId;

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddAdToCart(string userId, int adId)
        {
            var adBuyer = new AdBuyer
            {
                BuyerId = userId,
                AdId = adId
            };

            await dbContext.AdBuyers.AddAsync(adBuyer);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveAdFromCart(string userId, int adId)
        {
            var adBuyer = await dbContext.AdBuyers
                     .FirstOrDefaultAsync(ab => ab.BuyerId == userId && ab.AdId == adId);

            if (adBuyer != null)
            {
                dbContext.AdBuyers.Remove(adBuyer);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AllAdsViewModel>> GetMyCartAsync(string userId)
        {
            return await dbContext.AdBuyers
              .Where(ab => ab.BuyerId == userId)
              .Select(a => new AllAdsViewModel
              {
                  Id = a.Ad.Id,
                  Name = a.Ad.Name,
                  ImageUrl = a.Ad.ImageUrl,
                  Description = a.Ad.Description,
                  Price = a.Ad.Price,
                  Category = a.Ad.Category.Name,
                  Owner = a.Ad.Owner.UserName,
                  CreatedOn = a.Ad.CreatedOn.ToString("dd-MM-yyy H:mm"),
              }).ToListAsync();
        }
    }
}
