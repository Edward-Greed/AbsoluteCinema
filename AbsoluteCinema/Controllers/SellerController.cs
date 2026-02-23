using AbsoluteCinema.Models;
using BusinessLogicLayer;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Migrations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AbsoluteCinema.Controllers
{
    public class SellerController : Controller
    {
        private readonly ISellerService sellerService;
        private readonly IProductService productService;
        

        public SellerController(ISellerService sellerService, IProductService productService)
        {
            this.sellerService = sellerService;
            this.productService = productService;
            
        }

        public async Task<IActionResult> Products()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var seller = await sellerService.GetSellerByUserIdAsync(userId);

            if (seller == null)
                return RedirectToAction("BecomeSeller"); // optional

            var products = await sellerService.GetSellerProductsAsync(seller.SellerId);

            return View(products);
        }

        public async Task<IActionResult> CreateProduct()
        {
            var categories = await sellerService.GetCategoriesAsync();

            var vm = new ProductViewModel
            {
                Categories = categories
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await sellerService.GetCategoriesAsync();
                return View(vm);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var seller = await sellerService.GetSellerByUserIdAsync(userId);
            if (!ModelState.IsValid)
            {
                vm.Categories = await sellerService.GetCategoriesAsync();
                return View(vm);
            }

            var product = new ProductModel
            {
                Name = vm.Name,
                Description = vm.Description,
                Price = vm.Price,
                QuantityAvailable = vm.QuantityAvailable,
                ImagePath = vm.ImagePath,
                CategoryId = vm.CategoryId,
                SellerId = seller.SellerId
            };

            await sellerService.CreateProductAsync(product);

            return RedirectToAction("Products");
        }
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await sellerService.GetProductAsync(id);

            if (product == null)
                return NotFound();

            var categories = await sellerService.GetCategoriesAsync();

            var vm = new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                QuantityAvailable = product.QuantityAvailable,
                ImagePath = product.ImagePath,
                CategoryId = product.CategoryId,
                Categories = categories
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await sellerService.GetCategoriesAsync();
                return View(vm);
            }

            var product = await sellerService.GetProductAsync(vm.ProductId);

            if (product == null)
                return NotFound();

            product.Name = vm.Name;
            product.Description = vm.Description;
            product.Price = vm.Price;
            product.QuantityAvailable = vm.QuantityAvailable;
            product.ImagePath = vm.ImagePath;
            product.CategoryId = vm.CategoryId;

            await sellerService.UpdateProductAsync(product);

            return RedirectToAction("Products");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var userId = int.Parse(
    User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
);

            await productService.DeleteProductAsync(id, userId);

            return RedirectToAction("Products");
        }

        public async Task<IActionResult> PromoCodes()
        {
            var promos = await sellerService.GetPromoCodesAsync();
            return View(promos);
        }

        public IActionResult CreatePromo()
        {
            return View(new PromoCodeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePromo(PromoCodeViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var promo = new PromoCodeModel
            {
                Code = vm.Code,
                DiscountPercentage = vm.DiscountPercentage,
                ExpirationDate = vm.ExpirationDate,
                IsActive = vm.IsActive
            };

            await sellerService.CreatePromoCodeAsync(promo);

            return RedirectToAction("PromoCodes");
        }
    }
}
