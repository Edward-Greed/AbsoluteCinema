using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class SellerModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SellerId { get; set; }
        //foreign key
        public int ProductId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int phoneNumber { get; set; }
        public string Street { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public int Zip { get; set; }
        public string status { get; set; }

        public ICollection<ProductModel> Products { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
    }
}
