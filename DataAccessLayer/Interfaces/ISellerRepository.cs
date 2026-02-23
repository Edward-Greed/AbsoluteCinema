using DataAccessLayer.Entities;
using DataAccessLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ISellerRepository
    {
        Task<List<ProductModel>> GetAllProductsAsync();
        Task<ProductModel> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductModel product);
        Task UpdateProductAsync(ProductModel product);

        Task<List<PromoCodeModel>> GetAllPromoCodesAsync();
        Task AddPromoCodeAsync(PromoCodeModel promo);
        //new
        Task<List<ProductModel>> GetProductsBySellerAsync(int sellerId);
        Task<List<CategoryModel>> GetCategoriesAsync();
        Task<SellerModel> GetSellerByUserIdAsync(int userId);
        //sus
        //Task<List<PromoCodeModel>> GetPromoCodesAsync();

    }
}
