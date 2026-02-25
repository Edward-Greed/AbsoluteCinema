using AbsoluteCinema.Models;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AbsoluteCinema.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICartService cartService;
        public ProductController(IProductService productService, ICartService cartService)
        {
            this.cartService = cartService;
            this.productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetProductModelsAsync();

            return View(products);
        }
    }
}
