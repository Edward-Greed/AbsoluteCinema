using DataAccessLayer.Entities;
using DataAccessLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface ISellerService
    {
        Task<List<ProductModel>> GetProductsAsync();
        Task<ProductModel> GetProductAsync(int id);
        Task CreateProductAsync(ProductModel product);
        Task UpdateProductAsync(ProductModel product);

        Task<List<PromoCodeModel>> GetPromoCodesAsync();
        Task CreatePromoCodeAsync(PromoCodeModel promo);
        //new
        Task<List<ProductModel>> GetSellerProductsAsync(int sellerId);
        Task<List<CategoryModel>> GetCategoriesAsync();
        Task<SellerModel> GetSellerByUserIdAsync(int userId);
    }
}
