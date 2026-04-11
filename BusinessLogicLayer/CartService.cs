using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task AddToCartAsync(int userId, int productId)
        {
            await _cartRepository.AddToCartAsync(userId, productId);
        }

        public async Task<CartModel> GetCartAsync(int userId)
        {
            return await _cartRepository.GetCartByUserIdAsync(userId);
        }
        public async Task<int> GetCartItemCountAsync(int userId)
        {
            return await _cartRepository.GetCartItemCountAsync(userId);
        }
        public async Task UpdateQuantityAsync(int userId, int productId, int quantity)
        {
            await _cartRepository.UpdateQuantityAsync(userId, productId, quantity);
        }

        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            await _cartRepository.RemoveFromCartAsync(userId, productId);
        }
        public async Task<bool> ApplyPromoCodeAsync(int userId, string code)
        {
              return await _cartRepository.ApplyPromoCodeAsync(userId, code);
        }
        public async Task RemovePromoCodeAsync(int userId)
        {
            await _cartRepository.RemovePromoCodeAsync(userId);
        }
        public async Task ClearCartAsync(int userId)
        {
           await _cartRepository.ClearCartAsync(userId);
        }

    }
}
