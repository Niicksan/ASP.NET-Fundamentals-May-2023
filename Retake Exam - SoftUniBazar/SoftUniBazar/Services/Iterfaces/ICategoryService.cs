using SoftUniBazar.Models.Ad;

namespace SoftUniBazar.Services.Iterfaces
{
    public interface ICategoryService
    {
        Task<AdFormViewModel> GetAllCategoriesModelAsync();
    }
}
