using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class SellerService : ISellerService
    {
        private readonly ISellerRepository sellerRepository;

        public SellerService(ISellerRepository sellerRepository)
        {
            this.sellerRepository = sellerRepository;
        }

        public async Task<List<ProductModel>> GetProductsAsync()
        {
            return await sellerRepository.GetAllProductsAsync();
        }

        public async Task<ProductModel> GetProductAsync(int id)
        {
            return await sellerRepository.GetProductByIdAsync(id);
        }

        public async Task CreateProductAsync(ProductModel product)
        {
            await sellerRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(ProductModel product)
        {
            await sellerRepository.UpdateProductAsync(product);
        }

        public async Task<List<PromoCodeModel>> GetPromoCodesAsync()
        {
            return await sellerRepository.GetAllPromoCodesAsync();
        }

        public async Task CreatePromoCodeAsync(PromoCodeModel promo)
        {
            await sellerRepository.AddPromoCodeAsync(promo);
        }
        public async Task<List<ProductModel>> GetSellerProductsAsync(int sellerId)
        {
            return await sellerRepository.GetProductsBySellerAsync(sellerId);
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync()
        {
            return await sellerRepository.GetCategoriesAsync();
        }
        public async Task<SellerModel> GetSellerByUserIdAsync(int userId)
        {
            return await sellerRepository.GetSellerByUserIdAsync(userId);
        }
    }
}
