using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using WebApplication1.Models;
using static System.Formats.Asn1.AsnWriter;

namespace WebApplication1.EntityFrameworkCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Bire-çok ilişki
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Store)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.StoreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
