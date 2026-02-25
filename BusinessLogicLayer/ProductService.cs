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
        private readonly ISellerRepository sellerRepository;

        public ProductService(IProductRepository productRepository, ISellerRepository sellerRepository)
        {
            this.productRepository = productRepository;
            this.sellerRepository = sellerRepository;
        }
        
        public async Task<IEnumerable<ProductModel>> GetProductModelsAsync()
        {
            return await productRepository.GetProductModelsAsync();
        }
        public async Task DeleteProductAsync(int productId, int userId)
        {
            var seller = await sellerRepository.GetSellerByUserIdAsync(userId);
            if (seller == null) return;

            var product = await productRepository.GetByIdAsync(productId);
            if (product == null) return;

            if (product.SellerId != seller.SellerId)
                return;

            await productRepository.DeleteAsync(product);
        }
    }
}
