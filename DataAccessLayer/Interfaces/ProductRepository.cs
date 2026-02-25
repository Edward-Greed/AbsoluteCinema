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
        public async Task<ProductModel> GetByIdAsync(int productId)
        {
            return await productDbContext.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task DeleteAsync(ProductModel product)
        {
            productDbContext.Products.Remove(product);
            await productDbContext.SaveChangesAsync();
        }
    }
}
