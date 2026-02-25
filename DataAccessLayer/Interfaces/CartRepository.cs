using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public class CartRepository : ICartRepository
    {
        private readonly AbsoluteCinemaDbContext CartDbContext;

        public CartRepository(AbsoluteCinemaDbContext CartDbContext)
        {
            this.CartDbContext = CartDbContext;
        }

        public async Task AddToCartAsync(int userId, int productId)
        {
            var cart = await CartDbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new CartModel { UserId = userId };
                CartDbContext.Carts.Add(cart);
                await CartDbContext.SaveChangesAsync();
            }

            
            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.CartItems.Add(new CartItemModel
                {
                    ProductId = productId,
                    Quantity = 1
                });
            }

            await CartDbContext.SaveChangesAsync();
        }

        public async Task<CartModel> GetCartByUserIdAsync(int userId)
        {
            return await CartDbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .Include(c => c.PromoCode)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<int> GetCartItemCountAsync(int userId)
        {
            var cart = await CartDbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return 0;

            return cart.CartItems.Sum(ci => ci.Quantity);
        }
        public async Task UpdateQuantityAsync(int userId, int productId, int quantity)
        {
            var cart = await CartDbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return;

            var item = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (item == null)
                return;

            item.Quantity = quantity;

            await CartDbContext.SaveChangesAsync();
        }
        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await CartDbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return;

            var item = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (item == null)
                return;

            CartDbContext.CartItems.Remove(item);

            await CartDbContext.SaveChangesAsync();
        }
        public async Task<bool> ApplyPromoCodeAsync(int userId, string code)
        {
            var cart = await CartDbContext.Carts
                .Include(c => c.PromoCode)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return false;

            var promo = await CartDbContext.PromoCodes
                .FirstOrDefaultAsync(p =>
                    p.Code == code &&
                    p.IsActive &&
                    p.ExpirationDate > DateTime.UtcNow);

            if (promo == null)
                return false;

            cart.PromoCodeId = promo.Id;

            await CartDbContext.SaveChangesAsync();

            return true;
        }
        public async Task RemovePromoCodeAsync(int userId)
        {
            var cart = await CartDbContext.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return;

            cart.PromoCodeId = null;

            await CartDbContext.SaveChangesAsync();
        }
    }

}
