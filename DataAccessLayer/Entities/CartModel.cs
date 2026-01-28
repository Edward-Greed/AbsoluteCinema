using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class CartModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        
        //foreign key
        public int CustomerId { get; set; }
        public int Productid { get; set; }
        public int quantity { get; set; }
        public string wishlist { get; set; }
        public ICollection<ProductModel> Products { get; set; }



    }
}
