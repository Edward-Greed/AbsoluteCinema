using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetProductModelsAsync();

        Task<List<ProductModel>> GetAllAsync();
        Task<ProductModel> GetByIdAsync(int id);
        Task CreateAsync(ProductModel product);
        Task UpdateAsync(ProductModel product);
        Task DeleteAsync(int id);
       
        Task<IEnumerable<ProductModel>> GetRelatedProductsAsync(int categoryId, int productId);
    }
}
