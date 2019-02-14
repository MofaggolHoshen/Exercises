using EntityFrameworkExcercises.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkExcercises
{
    public class MyDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public MyDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EfExcercise;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new Student { Id = 1, FirstName = "Mofaggol", LastName = "Hoshen", Department = "Information Technology", UniversityName = "FH Frankfurnt" });
            base.OnModelCreating(modelBuilder);
        }
    }
}
