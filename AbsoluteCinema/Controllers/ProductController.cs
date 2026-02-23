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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdClaim.Value);

            await cartService.AddToCartAsync(userId, productId);

            return RedirectToAction("Index");
        }
    }
}
