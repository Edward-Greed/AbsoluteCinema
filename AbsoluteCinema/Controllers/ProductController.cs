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
    }
}
