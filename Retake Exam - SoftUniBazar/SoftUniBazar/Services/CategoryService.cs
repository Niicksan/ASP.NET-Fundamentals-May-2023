using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Models.Ad;
using SoftUniBazar.Models.Category;
using SoftUniBazar.Services.Iterfaces;

namespace SoftUniBazar.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BazarDbContext dbContext;

        public CategoryService(BazarDbContext dbContex)
        {
            this.dbContext = dbContex;
        }

        public async Task<AdFormViewModel> GetAllCategoriesModelAsync()
        {
            var categories = await dbContext
               .Categories
               .Select(c => new CategorySelectViewModel
               {
                   Id = c.Id,
                   Name = c.Name
               }).ToListAsync();

            var model = new AdFormViewModel
            {
                Categories = categories
            };

            return model;
        }
    }
}
