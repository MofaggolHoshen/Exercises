using EShop.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Services
{
    public class EShopDbContext : DbContext
    {
        public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options)
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                        .HasIndex(i => i.NumberOnCard)
                        .IsUnique(true);

            modelBuilder.Entity<ProductDiscount>()
                        .HasOne(i => i.Product)
                        .WithMany(i => i.Discounts);

            var mutableForeignKeys = modelBuilder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetForeignKeys());

            foreach (var fk in mutableForeignKeys)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
    }
}
