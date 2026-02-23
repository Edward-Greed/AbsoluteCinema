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
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<OrderModel> Orders{ get; set; }  
        public DbSet<UserModel> Users {  get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }
        public DbSet<CartItemModel> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerModel>()
                .HasIndex(c => c.UserId)
                .IsUnique();
            modelBuilder.Entity<CartItemModel>()
                .HasIndex(ci => new { ci.CartId, ci.ProductId })
                .IsUnique();
            modelBuilder.Entity<OrderItemModel>()
                .HasIndex(oi => new { oi.OrderId, oi.ProductId })
                .IsUnique();

            modelBuilder.Entity<CartItemModel>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CartItemModel>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
