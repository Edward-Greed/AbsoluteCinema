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
        public int Customerid {  get; set; }
        public int UserId {  get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public int phoneNumber { get; set; }
        public string Street { get; set; }
        public int state { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string status { get; set; }
        public ICollection<OrderModel> orders { get; set; }





    }
}
