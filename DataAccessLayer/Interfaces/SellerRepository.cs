using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public class SellerRepository : ISellerRepository
    {
        private readonly AbsoluteCinemaDbContext SellerDbContext;

        public SellerRepository(AbsoluteCinemaDbContext SellerDbContext)
        {
            this.SellerDbContext = SellerDbContext;
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            return await SellerDbContext.Products.ToListAsync();
        }

        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            return await SellerDbContext.Products.FindAsync(id);
        }

        public async Task AddProductAsync(ProductModel product)
        {
            SellerDbContext.Products.Add(product);
            await SellerDbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductModel product)
        {
            SellerDbContext.Products.Update(product);
            await SellerDbContext.SaveChangesAsync();
        }

        public async Task<List<PromoCodeModel>> GetAllPromoCodesAsync()
        {
            return await SellerDbContext.PromoCodes.ToListAsync();
        }

        public async Task AddPromoCodeAsync(PromoCodeModel promo)
        {
            SellerDbContext.PromoCodes.Add(promo);
            await SellerDbContext.SaveChangesAsync();
        }
        public async Task<List<ProductModel>> GetProductsBySellerAsync(int sellerId)
        {
            return await SellerDbContext.Products
                .Where(p => p.SellerId == sellerId)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync()
        {
            return await SellerDbContext.Categories.ToListAsync();
        }
        public async Task<SellerModel> GetSellerByUserIdAsync(int userId)
        {
            return await SellerDbContext.Sellers
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }
    }
}
