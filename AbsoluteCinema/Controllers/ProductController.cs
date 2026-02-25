using AbsoluteCinema.Models;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace AbsoluteCinema.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetProductModelsAsync();

            return View(products);
        }
        public async Task<IActionResult> Details(int id) 
        { 
            var product = await productService.GetProductByIdAsync(id);

            if (product == null) 
            {
                return NotFound();
            }

            var relatedProducts = await productService.GetRelatedProductsAsync(product.CategoryId, product.ProductId);
            
            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                RelatedProducts = relatedProducts
            };

            return View(viewModel);
        }
    }
}
