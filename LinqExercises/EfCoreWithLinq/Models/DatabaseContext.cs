using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EfCoreWithLinq.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<House> Houses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=linq;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<House>().HasData(new House()
            {
                Id = 1,
                Number = 1,
                StreetId = 1
            });
            modelBuilder.Entity<House>().HasData(new House()
            {
                Id = 2,
                Number = 2,
                StreetId = 1
            });
            modelBuilder.Entity<House>().HasData(new House()
            {
                Id = 3,
                Number = 3,
                StreetId = 1
            });
            modelBuilder.Entity<House>().HasData(new House()
            {
                Id = 4,
                Number = 4,
                StreetId = 1
            });

            modelBuilder.Entity<House>().HasData(new House()
            {
                Id = 5,
                Number = 5,
                StreetId = 1
            });

            modelBuilder.Entity<Street>().HasData(new Street()
            {
                Id = 1,
                AddressId = 1
            });

            modelBuilder.Entity<Address>().HasData(new Address()
            {
                Id = 1,
                Peopleid = 1
            });

            modelBuilder.Entity<People>().HasData(new People()
            {
                Id = 1,
                Name = "Mofaggol",
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
