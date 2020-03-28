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
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EFCore-smm4;Trusted_Connection=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //----------------------------------- Items---------------------------------------

            modelBuilder.Entity<Item>().Property(b => b.Id).ValueGeneratedNever();
            modelBuilder.Entity<Item>().Property(b => b.PoQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.AsnQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.GrnQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.Price).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.Quantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.TotalPrice).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.Note).HasColumnType("text").HasDefaultValue("Note :  ");

            modelBuilder.Entity<Item>()
                .HasOne<ProductMaster>(s => s.ProductMaster)
                .WithMany(g => g.Items)
                .HasForeignKey(s => s.ProductMaster_fk);

            modelBuilder.Entity<Item>()
                .HasOne<PurchaseOrder>(s => s.PurchaseOrder)
                .WithMany(g => g.Items)
                .HasForeignKey(s => s.Po_fk);

            //----------------------------------- Product Master------------------------------


            modelBuilder.Entity<ProductMaster>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

            });

            //----------------------------------- Vendor--------------------------------------

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e=>e.Id).ValueGeneratedNever();
                entity.Property(e => e.Note).HasColumnName("Note");
                entity.Property(e => e.Id).HasColumnName("ID");
                
            });

            //----------------------------------- Purchase Order------------------------------

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne<Vendor>(s => s.Vendor)
                    .WithMany(g => g.PurchaseOrders)
                    .HasForeignKey(s => s.Vendor_fk);

            });

            //----------------------------------- new ---------------------------------------
        }


    }
}
