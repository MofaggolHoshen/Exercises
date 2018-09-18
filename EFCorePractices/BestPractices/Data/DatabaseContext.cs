using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BestPractices.Models;

namespace BestPractices.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Child>()
            //    .HasOne(c => c.Parent)
            //    .WithOne(p => p.Child)
            //    .OnDelete(DeleteBehavior.Restrict);

            var foreignKeys = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys());

            foreach (var f in foreignKeys)
            {
                f.DeleteBehavior = DeleteBehavior.Restrict;
            }

            var parents = new List<Parent>()
                {
                    new Parent()
                    {
                        Id = 1,
                        Name = "One",
                        Address = "Frankfurt"
                    },
                    new Parent()
                    {
                        Id = 2,
                        Name = "Two",
                        Address = "Frankfurt"
                    },
                     new Parent()
                    {
                        Id = 3,
                        Name = "Three",
                        Address = "Frankfurt"
                    }
                }.ToArray();

            modelBuilder.Entity<Parent>()
                .HasData(parents);

            var childs = new List<Child>()
                {
                    new Child()
                    {
                        Id = 1,
                        ChildName = "One",
                        ParentId = 1
                    },
                    new Child()
                    {
                        Id = 2,
                        ChildName = "Two",
                        ParentId = 2
                    },
                    new Child()
                    {
                        Id = 3,
                        ChildName = "Three",
                        ParentId = 2
                    },
                    new Child()
                    {
                        Id = 4,
                        ChildName = "Four",
                        ParentId = 1
                    }
                }.ToArray();

            modelBuilder.Entity<Child>()
                .HasData(childs);

            var addresses = new List<Address>()
            {
                new Address()
                {
                    Id = 1,
                    State = "Hessen",
                    ChildId = 1
                },
                new Address()
                {
                    Id = 2,
                    State = "Hessen",
                    ChildId = 2
                }
            }.ToArray();
            modelBuilder.Entity<Address>()
               .HasData(addresses);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Parent> Parents { get; set; }
        public DbSet<Child> Children { get; set; }
    }
}
