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
        Task DeleteAsync(ProductModel product);
        Task<ProductModel> GetByIdAsync(int productId);
    }
}
