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
    public class ProductRepository : IProductRepository
    {
        private readonly AbsoluteCinemaDbContext productDbContext;

        public ProductRepository(AbsoluteCinemaDbContext productDbContext) 
        {
            this.productDbContext = productDbContext;
        }
        public async Task<IEnumerable<ProductModel>> GetProductModelsAsync() 
        { 
            return await productDbContext.Products.Include(p => p.Category).ToListAsync();
        }
        public async Task<List<ProductModel>> GetAllAsync()
        {
            return await productDbContext.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {
            return await productDbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task AddAsync(ProductModel product)
        {
            productDbContext.Products.Add(product);
            await productDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductModel product)
        {
            productDbContext.Products.Update(product);
            await productDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductModel product)
        {
            productDbContext.Products.Remove(product);
            await productDbContext.SaveChangesAsync();
        }
       
        public async Task<IEnumerable<ProductModel>> GetRelatedProductsAsync(int categoryId, int productId)
        {
            return await productDbContext.Products.Where(p => p.CategoryId == categoryId && p.ProductId != productId).Include(p => p.Category).Take(2).ToListAsync();
        }
    }
}
