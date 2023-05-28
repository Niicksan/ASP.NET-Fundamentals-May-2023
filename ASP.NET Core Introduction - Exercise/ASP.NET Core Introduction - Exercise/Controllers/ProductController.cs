using System.Text;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace ASP.NET_Core_Introduction___Exercise.Controllers
{
    using ViewModels.Product;
    using static Seeding.ProductsData;

    public class ProductController : Controller
    {
        [Route("/products/all")]
        public IActionResult AllProducts(string keyword)
        {
            if (String.IsNullOrWhiteSpace(keyword))
            {
                return View(Products);
            }

            IEnumerable<ProductViewModel> productsAfterSearch = Products
                .Where(p => p.Name.ToLower().Contains(keyword.ToLower()))
                .ToArray();
            return View(productsAfterSearch);
        }

        [Route("/products/details/{id?}")]
        public IActionResult ProductById(string id)
        {
            ProductViewModel? product = Products
                .FirstOrDefault(p => p.Id.ToString().Equals(id));

            if (product == null)
            {
                return this.RedirectToAction("AllProducts");
            }

            return this.View(product);
        }

        public IActionResult AllAsJson()
        {
            return Json(Products, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
        }

        public IActionResult DownloadProductsInfo()
        {
            // This should be in separate class
            StringBuilder sb = new StringBuilder();
            foreach (var product in Products)
            {
                sb
                    .AppendLine($"Product with Id: {product.Id}")
                    .AppendLine($"## Product Name: {product.Name}")
                    .AppendLine($"## Price: {product.Price:f2}$")
                    .AppendLine($"-------------------------------");
            }

            Response.Headers
                .Add(HeaderNames.ContentDisposition, "attachment;filename=products.txt");

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/plain");
        }
    }
}
