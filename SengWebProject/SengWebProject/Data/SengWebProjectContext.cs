using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAcess2;

namespace SengWebProject.Data
{
    public class SengWebProjectContext : DbContext
    {
        public SengWebProjectContext(DbContextOptions<SengWebProjectContext> options)
            : base(options)
        {
        }

        // DbSet declarations for all entities
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Employee> Employees { get; set; } = default!;
        public DbSet<Inventory> Inventories { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Shipment> Shipments { get; set; } = default!;
        public DbSet<Supplier> Suppliers { get; set; } = default!;

        // OnModelCreating override for custom configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Customer entity
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.HasKey(c => c.CustomerID);
                entity.Property(c => c.Name).IsRequired();
                entity.Property(c => c.ContactInfo).IsRequired(false);
                entity.Property(c => c.Address).IsRequired(false);
                entity.Property(c => c.LastModified).HasColumnType("datetime2");
            });

            // Configure the Employee entity
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");
                entity.HasKey(e => e.EmployeeID);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Role).IsRequired(false);
                entity.Property(e => e.ContactInfo).IsRequired(false);
                entity.Property(e => e.LastModified).HasColumnType("datetime2");
            });

            // Other entity configurations (e.g., Inventory, Order, etc.)
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");
                entity.HasKey(i => i.InventoryID);
                entity.Property(i => i.QuantityAvailable).IsRequired();
                entity.Property(i => i.LastModified).HasColumnType("datetime2");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");
                entity.HasKey(o => o.OrderID);
                entity.Property(o => o.OrderDate).IsRequired();
                entity.Property(o => o.LastModified).HasColumnType("datetime2");
                entity.HasOne(o => o.Customer)
                      .WithMany(c => c.Orders)
                      .HasForeignKey(o => o.CustomerID);
                entity.HasOne(o => o.Employee)
                      .WithMany(e => e.Orders)
                      .HasForeignKey(o => o.EmployeeID);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(p => p.ProductID);
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.LastModified).HasColumnType("datetime2");
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("Shipment");
                entity.HasKey(s => s.ShipmentID);
                entity.Property(s => s.ShipmentDate).IsRequired();
                entity.Property(s => s.LastModified).HasColumnType("datetime2");
                entity.HasOne(s => s.Order)
                      .WithMany(o => o.Shipments)
                      .HasForeignKey(s => s.OrderID);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");
                entity.HasKey(s => s.SupplierID);
                entity.Property(s => s.Name).IsRequired();
                entity.Property(s => s.LastModified).HasColumnType("datetime2");
            });
        }
    }
}
