using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetProductModelsAsync();
        Task<List<ProductModel>> GetAllAsync();
        Task<ProductModel> GetByIdAsync(int id);
        Task AddAsync(ProductModel product);
        Task UpdateAsync(ProductModel product);
        Task DeleteAsync(ProductModel product);
       
        Task<IEnumerable<ProductModel>> GetRelatedProductsAsync(int categoryId, int productId);
    }
}
