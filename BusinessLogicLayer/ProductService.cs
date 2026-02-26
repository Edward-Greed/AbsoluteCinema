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
        public async Task<List<ProductModel>> GetAllAsync()
        {
            return await productRepository.GetAllAsync();
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {
            return await productRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(ProductModel product)
        {
            await productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(ProductModel product)
        {
            await productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
                return;

            await productRepository.DeleteAsync(product);
        }

        public async Task<IEnumerable<ProductModel>> GetRelatedProductsAsync(int categoryId, int productId)
        {
            return await productRepository.GetRelatedProductsAsync(categoryId, productId);
        }
    }
}
