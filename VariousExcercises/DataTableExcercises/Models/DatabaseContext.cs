using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTableExcercises.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            for (int i = 1; i < 50000; i++)
            {
                modelBuilder.Entity<Client>().HasData(new Client()
                {
                    Id = i,
                    Name = i + "Name",
                    Address = i + "Address"
                });
            }
        }
    }
}
