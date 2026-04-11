using AbsoluteCinema.Models;
using BusinessLogicLayer;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AbsoluteCinema.Controllers
{
    [Authorize]
public class CartController : Controller
{
    private readonly ICartService cartService;
    private readonly AbsoluteCinemaDbContext _context;

        public CartController(ICartService cartService, AbsoluteCinemaDbContext context)
        {
            this.cartService = cartService;
            _context = context;
        }
        private int GetUserId()
    {
       return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }

        public async Task<IActionResult> Index()
    {
        int userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)
        );

            var cart = await cartService.GetCartAsync(userId);
            return View(cart);
    }
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                await cartService.RemoveFromCartAsync(GetUserId(), productId);
            }
            else
            {
                await cartService.UpdateQuantityAsync(GetUserId(), productId, quantity);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            await cartService.RemoveFromCartAsync(GetUserId(), productId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> ApplyPromo(string code)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            bool applied = await cartService.ApplyPromoCodeAsync(userId, code);

            if (!applied)
            {
                TempData["PromoError"] = "Promo code is invalid";
            }
            else
            {
                TempData["PromoSuccess"] = "Promo code applied successfully!";
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> RemovePromo()
        {
            await cartService.RemovePromoCodeAsync(GetUserId());
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Checkout()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
                return RedirectToAction("Index"); // back to cart

            return View(cart.CartItems);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(
    string ShippingAddress,
    string PromoCode,
    decimal Subtotal,
    decimal Tax,
    decimal Shipping,
    decimal Total)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
                return RedirectToAction("Index");

            // 🎟 OPTIONAL: Apply promo (simple version)
            if (!string.IsNullOrEmpty(PromoCode))
            {
                var promo = await _context.PromoCodes
                    .FirstOrDefaultAsync(p =>
                        p.Code == PromoCode &&
                        p.IsActive &&
                        p.ExpirationDate > DateTime.UtcNow);

                if (promo != null)
                {
                    Total -= promo.DiscountPercentage; // simple discount
                }
            }

            var order = new OrderModel
            {
                CustomerId = userId,
                OrderDate = DateTime.UtcNow,
                OrderStatus = "Pending",
                TrackingNumber = Guid.NewGuid().ToString().Substring(0, 10),
                ShippingDate = DateTime.UtcNow.AddDays(2),
                ShippingAddress = ShippingAddress,
                TotalAmount = Total,
                OrderItems = new List<OrderItemModel>()
            };

            foreach (var item in cart.CartItems)
            {
                order.OrderItems.Add(new OrderItemModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    PurchasePrice = item.Product.Price
                });

                item.Product.QuantityAvailable -= item.Quantity;
            }

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cart.CartItems);

            await _context.SaveChangesAsync();

            return RedirectToAction("CheckoutSuccess");
        }
        public IActionResult CheckoutSuccess()
        {
            return View();
        }
        public async Task<IActionResult> MyOrders()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var orders = await _context.Orders
                .Where(o => o.CustomerId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }
    }

}

