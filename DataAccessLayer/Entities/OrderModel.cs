using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class OrderModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
        public int SellerId { get; set; }
        public SellerModel Seller { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime ShippingDate { get; set; }
        public string ShippingAddress { get; set; }
        public ICollection<OrderItemModel> OrderItems { get; set; }
    }
}
