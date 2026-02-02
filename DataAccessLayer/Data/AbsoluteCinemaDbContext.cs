using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class AbsoluteCinemaDbContext : DbContext
    {
        public AbsoluteCinemaDbContext(DbContextOptions<AbsoluteCinemaDbContext> options) : base(options)
        {
        }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<CartModel> Carts { get; set; }
        public DbSet<CustomerModel>Customers { get; set; }
        public DbSet<OrderModel> Orders{ get; set; }  
        public DbSet<SellerModel> Sellers { get; set; }
        public DbSet<UserModel> Users {  get; set; }
    }
}
