using DataAccessLayer.Entities;

namespace AbsoluteCinema.Models
{
    public class ProductDetailsViewModel
    {
        public ProductModel Product { get; set; }
        public IEnumerable<ProductModel> RelatedProducts { get; set; }
    }
}
