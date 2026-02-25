using AbsoluteCinema.Models;
using BusinessLogicLayer;
using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AbsoluteCinema.Controllers
{
    [Authorize]
public class CartController : Controller
{
    private readonly ICartService cartService;
    
    public CartController(ICartService cartService)
    {
        this.cartService = cartService;
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
    }

}

