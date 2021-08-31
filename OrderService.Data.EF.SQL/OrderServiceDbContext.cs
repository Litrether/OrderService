using Microsoft.EntityFrameworkCore;
using OrderService.Data.Domain.Models;
using System;

namespace OrderService.Data.EF.SQL
{
    public class OrderServiceDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
     
        public OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            base.OnModelCreating(modelBuilder);
    }
}
