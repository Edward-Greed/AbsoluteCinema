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
        Task<ProductModel> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductModel>> GetRelatedProductsAsync(int categoryId, int productId);
    }

}
