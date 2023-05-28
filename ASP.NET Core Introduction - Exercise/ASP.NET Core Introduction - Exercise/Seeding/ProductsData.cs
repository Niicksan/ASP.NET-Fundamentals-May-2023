using ASP.NET_Core_Introduction___Exercise.ViewModels.Product;

namespace ASP.NET_Core_Introduction___Exercise.Seeding
{
    using ViewModels.Product;

    public class ProductsData
    {
        public static IEnumerable<ProductViewModel> Products =
            new List<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    Id = 1,
                    Name = "Cheese",
                    Price = 7.00m
                },
                new ProductViewModel()
                {
                    Id = 2,
                    Name = "Ham",
                    Price = 5.50m
                },
                new ProductViewModel()
                {
                    Id = 3,
                    Name = "Bread",
                    Price = 1.50m
                }
            };
    }
}
