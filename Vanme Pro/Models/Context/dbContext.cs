﻿using System;
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
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductInventoryWarehouse> ProductInventoryWarehouses { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<Province> Provinces  { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EFCore-smm24;Trusted_Connection=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //----------------------------------- Items---------------------------------------

            modelBuilder.Entity<Item>().Property(b => b.Id).ValueGeneratedNever();
            modelBuilder.Entity<Item>().Property(b => b.PoQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.AsnQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.GrnQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.PoPrice).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.PoQuantity).HasDefaultValue(0);
            modelBuilder.Entity<Item>().Property(b => b.Checked).HasDefaultValue(false);
            modelBuilder.Entity<Item>().Property(b => b.Alert).HasDefaultValue(false);
            modelBuilder.Entity<Item>().Ignore(p => p.Price);
            modelBuilder.Entity<Item>().Ignore(p => p.PreviousQuantity);
            modelBuilder.Entity<Item>().Ignore(p => p.CurrentQuantity);
            modelBuilder.Entity<Item>().Ignore(p => p.TotalItemPrice);
            modelBuilder.Entity<Item>().Property(a => a.RowVersion).IsRowVersion();
            //modelBuilder.Entity<Item>().HasData(new Item
            //{
            //    Id = 100, Po_fk = 1, ProductMaster_fk = 1, PoQuantity = 0, AsnQuantity = 0, GrnQuantity = 0,
            //    PoPrice = 0, AsnPrice = 0, Cost = 0, PoItemsPrice = 0, AsnItemsPrice = 0, Alert = false, Checked = false
            //});

            // modelBuilder.Entity<Item>().Property(b => b.Note).HasColumnType("text").HasDefaultValue("Note :  ");

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
                entity.Ignore(e => e.TempBalance);
                entity.Property(b => b.StockOnHand).HasDefaultValue(0);
                entity.Property(b => b.GoodsReserved).HasDefaultValue(0);
                entity.Property(b => b.Income).HasDefaultValue(0);
                entity.Property(b => b.Outcome).HasDefaultValue(0);

            });

            //----------------------------------- Vendor--------------------------------------

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e=>e.Id).ValueGeneratedNever();
                entity.Property(e => e.Note).HasColumnName("Note");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.HasData(new Vendor{Id = 1,Name = "ClubJummana"});
                entity.HasData(new Vendor{Id = 2,Name = "Anzir"});
                entity.HasData(new Vendor{Id = 3,Name = "Noman"});
                
            });

            //----------------------------------- Purchase Order------------------------------

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CreateOrder).HasDefaultValueSql("getdate()");
                entity.Property(a => a.RowVersion).IsRowVersion();
                //entity.HasData();

                entity.HasOne<Vendor>(s => s.Vendor)
                    .WithMany(g => g.PurchaseOrders)
                    .HasForeignKey(s => s.Vendor_fk);

                entity.HasOne<Warehouse>(s => s.ToWarehouse)
                    .WithMany(g => g.PoToWareHose)
                    .HasForeignKey(s => s.ToWarehouse_fk);
                entity.HasOne<Warehouse>(s => s.FromWarehouse)
                    .WithMany(g => g.POFromWarehouse)
                    .HasForeignKey(s => s.FromWarehouse_fk);


                entity.HasOne<User>(s => s.UserCreatePo)
                    .WithMany(g => g.CreatePo)
                    .HasForeignKey(s => s.ApprovePoUser_fk);
                entity.HasOne<User>(s => s.UserCreateAsn)
                    .WithMany(g => g.CreateAsn)
                    .HasForeignKey(s => s.ApproveAsnUser_fk);
                entity.HasOne<User>(s => s.UserCreateGrn)
                    .WithMany(g => g.CreateGrn)
                    .HasForeignKey(s => s.ApproveGrnUser_fk);
            });
            //----------------------------------- Warehouse--------------------------------------

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
              
            });

            //-----------------------------------     ProductInventoryWarehouse  ---------------------------------------

            modelBuilder.Entity<ProductInventoryWarehouse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(b => b.Inventory).HasDefaultValue(0);
                entity.Property(b => b.OnTheWayInventory).HasDefaultValue(0);

                entity.HasOne<ProductMaster>(s => s.ProductMaster)
                    .WithMany(g => g.ProductInventoryWarehouses)
                    .HasForeignKey(s => s.ProductMaster_fk);

                entity.HasOne<Warehouse>(s => s.Warehouse)
                    .WithMany(g => g.ProductInventoryWarehouses)
                    .HasForeignKey(s => s.Warehouse_fk);


            });


            //----------------------------------- User ---------------------------------------

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(b => b.IsAdmin).HasDefaultValue(false);
                entity.Property(b => b.IsCashier).HasDefaultValue(false);
                entity.Property(b => b.IsVisitor).HasDefaultValue(false);
                entity.Property(b => b.UserName).HasDefaultValue(123456);
                entity.Property(b => b.UserName).HasDefaultValue(0);
            });


            //----------------------------------- SaleOrder ---------------------------------------
            modelBuilder.Entity<SaleOrder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderedDate).HasColumnType("smalldatetime");
                entity.Property(e => e.CancelDate).HasColumnType("smalldatetime");
                entity.Ignore(e => e.TaxName);
                entity.Ignore(e => e.TaxRate);
                entity.Ignore(e => e.SubtotalwithServices);
                entity.Ignore(e => e.IsSaveDatabase);

                entity.HasOne<User>(s => s.User)
                    .WithMany(g => g.SaleOrders)
                    .HasForeignKey(s => s.Cashier_fk);

                entity.HasOne<Customer>(s => s.Customer)
                    .WithMany(g => g.SaleOrders)
                    .HasForeignKey(s => s.Customer_fk);

                entity.HasOne<Province>(s => s.TaxArea)
                    .WithMany(g => g.SaleOrders)
                    .HasForeignKey(s => s.TaxArea_fk);

                entity.HasOne<Warehouse>(s => s.Warehouse)
                    .WithMany(g => g.SaleOrders)
                    .HasForeignKey(s => s.Warehouse_fk);
            });

            //----------------------------------- Province ---------------------------------------
            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                //entity.Property(e => e.HST).HasColumnType("real");
                //entity.Property(e => e.GST).HasColumnType("real");
                //entity.Property(e => e.QST).HasColumnType("real");
            });
            //----------------------------------- Customer ---------------------------------------
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Ignore(p => p.FullName);

                entity.HasOne<User>(s => s.User)
                    .WithMany(g => g.Customers)
                    .HasForeignKey(s => s.CreatedBy_fk);
            });
            //----------------------------------- SoItem ---------------------------------------
            modelBuilder.Entity<SoItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne<SaleOrder>(s => s.SaleOrder)
                    .WithMany(g => g.SoItems)
                    .HasForeignKey(s => s.So_fk);

                entity.HasOne<ProductMaster>(s => s.ProductMaster)
                    .WithMany(g => g.SoItems)
                    .HasForeignKey(s => s.ProductMaster_fk);
            });
            //----------------------------------- new ---------------------------------------
        }


    }
}
