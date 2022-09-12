using EntityFrameworkExcercises.FluentApiSamples.Entities.OneToMany;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkExcercises.FluentApiSamples
{
    public class OneToManyDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        public OneToManyDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region One to Many
            //modelBuilder.Entity<Tenant>()
            //            .HasMany(t => t.Currencies)
            //            .WithOne(t => t.Tenant)
            //            .HasForeignKey(i => i.TenantId);

            // Otherway around
            modelBuilder.Entity<Currency>()
                        .HasOne(t => t.Tenant)
                        .WithMany(t => t.Currencies)
                        .HasForeignKey(i => i.TenantId);

            // Same type one to one 
            modelBuilder.Entity<Tenant>()
                        .HasOne(t => t.BaseCurrency);

            #endregion



            modelBuilder.Entity<Tenant>().HasData(new Tenant(1, "Tenant1"));
            modelBuilder.Entity<Tenant>().HasData(new Tenant(2, "Tenant2"));

            modelBuilder.Entity<Currency>().HasData(new Currency(1, "Eur", 1));
            modelBuilder.Entity<Currency>().HasData(new Currency(2, "Doller", 1));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Data Source=localhost;Initial Catalog=EfExcercise;Integrated Security=True
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=FluentApiSample;Integrated Security=True");
        }
    }
}
