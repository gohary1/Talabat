using Microsoft.EntityFrameworkCore;
using ProductData.Entites;
using ProductData.Entites.orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository
{
    public class StoreDpContext:DbContext
    {
        public StoreDpContext(DbContextOptions<StoreDpContext> options):base(options) 
        { 

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductType> Types { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethod { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

    }
}
