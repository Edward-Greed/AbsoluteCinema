using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface ICartService
    {
        Task AddToCartAsync(int userId, int productId);
        Task<CartModel> GetCartAsync(int userId);
        Task<int> GetCartItemCountAsync(int userId);
        Task UpdateQuantityAsync(int userId, int productId, int quantity);
        Task RemoveFromCartAsync(int userId, int productId);
        Task<bool> ApplyPromoCodeAsync(int userId, string code);
        Task RemovePromoCodeAsync(int userId);
        Task ClearCartAsync(int userId);
    }
}
