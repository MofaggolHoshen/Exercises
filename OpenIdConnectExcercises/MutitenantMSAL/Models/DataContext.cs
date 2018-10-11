using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MutitenantMSAL.Models
{
    public class DataContext : DbContext
    {

        private readonly AppTenant tenant;
        private readonly IHttpContextAccessor httpContext;
        
        public DataContext(AppTenant tenant, IHttpContextAccessor httpContext)
        {
            if (tenant != null)
            {
                this.tenant = tenant;
                this.httpContext = httpContext;
                Database.EnsureCreated();
            }
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (tenant != null )//&& httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                optionsBuilder.UseSqlServer(tenant.ConnectionString);
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }


        public DbSet<Post> Posts { get; set; }
        public DbSet<NetworkUnit> NetworkUnits { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
    }

}