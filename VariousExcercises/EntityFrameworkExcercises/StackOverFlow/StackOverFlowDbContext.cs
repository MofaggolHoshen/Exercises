using EntityFrameworkExcercises.StackOverFlow.Entities.UpdateOneToOneTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkExcercises.StackOverFlow
{
    public class StackOverFlowDbContext : DbContext
    {
        public DbSet<Step> Steps { get; set; }
        public DbSet<StepLevel> StepLevels { get; set; }

        public StackOverFlowDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=StackOverFlow;Integrated Security=True");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
