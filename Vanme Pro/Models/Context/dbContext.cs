using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vanme_Pro.Models.DomainModels;

namespace Vanme_Pro.Models.Context
{
    class dbContext:DbContext
    {

        public DbSet<ProductMaster> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Vendor> Vendors { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EFCore-smm;Trusted_Connection=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<ProductMaster>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Vendor>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ProductMaster>()
                .HasOne<Vendor>(s => s.Vendor)
                .WithMany(g => g.ProductMasters)
                .HasForeignKey(s => s.Vendor_fk);

            modelBuilder.Entity<Item>()
                .HasOne<ProductMaster>(s => s.ProductMaster)
                .WithMany(g => g.Items)
                .HasForeignKey(s => s.ProductMaster_fk);

            modelBuilder.Entity<Item>()
                .HasOne<PurchaseOrder>(s => s.PurchaseOrder)
                .WithMany(g => g.Items)
                .HasForeignKey(s => s.Po_fk);



            //----------------------------------- Items---------------------------------------

            modelBuilder.Entity<Item>().Property(b => b.Id).ValueGeneratedNever();
            modelBuilder.Entity<Item>().Property(b => b.PoQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.AsnQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.GrnQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.Price).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.Quantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.TotalPrice).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.Note).HasColumnType("text").HasDefaultValue("Note :  ");
              

            //----------------------------------- Product Master------------------------------
            //----------------------------------- Vendor--------------------------------------

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e=>e.Id).ValueGeneratedNever();
                entity.HasIndex(e => e.Id);
                entity.Property(e => e.Note).HasColumnName("Note");
                entity.Property(e => e.Id).HasColumnName("ID");
                
            });


            //----------------------------------- Purchase Order------------------------------
            //----------------------------------- new ---------------------------------------
        }


    }
}
