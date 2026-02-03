using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class CustomerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId {  get; set; }
        public int UserId {  get; set; }
        public UserModel User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Street { get; set; }
        public int State { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string Status { get; set; }
        public ICollection<CartModel> Carts { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
    }
}
