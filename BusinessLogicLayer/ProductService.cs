using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductModel>> GetProductModelsAsync()
        {
            return await productRepository.GetProductModelsAsync();
        }
        public async Task<ProductModel> GetProductByIdAsync(int id) 
        {
            return await productRepository.GetProductByIdAsync(id);
        }
        public async Task<IEnumerable<ProductModel>> GetRelatedProductsAsync(int categoryId, int productId) 
        {
            return await productRepository.GetRelatedProductsAsync(categoryId, productId);
        }
    }
}
