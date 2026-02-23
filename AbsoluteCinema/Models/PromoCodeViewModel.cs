using System.ComponentModel.DataAnnotations;

namespace AbsoluteCinema.Models
{
    public class PromoCodeViewModel
    {
        public int PromoCodeId { get; set; }
        [Required]

        public string Code { get; set; }
        [Required]

        public decimal DiscountPercentage { get; set; }
        [Required]

        public DateTime ExpirationDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
