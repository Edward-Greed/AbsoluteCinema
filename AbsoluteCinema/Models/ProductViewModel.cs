using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AbsoluteCinema.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int QuantityAvailable { get; set; }
        [Required]

        public string ImagePath { get; set; }
        [Required]

        public int CategoryId { get; set; }
        [ValidateNever]
        public List<CategoryModel> Categories { get; set; }
    }
}
