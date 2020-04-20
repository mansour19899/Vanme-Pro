﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vanme_Pro.Models.Context;

namespace Vanme_Pro.Migrations
{
    [DbContext(typeof(dbContext))]
    [Migration("20200418221737_smm192")]
    partial class smm192
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy_fk")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EditedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastSaleDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.Item", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool?>("Alert")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<decimal>("AsnItemsPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AsnPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("AsnQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<bool?>("Checked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Diffrent")
                        .HasColumnType("int");

                    b.Property<int>("GrnQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PoItemsPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PoPrice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<int>("PoQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("Po_fk")
                        .HasColumnType("int");

                    b.Property<int>("ProductMaster_fk")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Po_fk");

                    b.HasIndex("ProductMaster_fk");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.ProductInventoryWarehouse", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Aile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Inventory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int?>("OnTheWayInventory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int?>("ProductMaster_fk")
                        .HasColumnType("int");

                    b.Property<int?>("Warehouse_fk")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductMaster_fk");

                    b.HasIndex("Warehouse_fk");

                    b.ToTable("ProductInventoryWarehouses");
                });

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.ProductMaster", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("FobPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Income")
                        .HasColumnType("int");

                    b.Property<int>("Inventory")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdateInventory")
                        .HasColumnType("datetime2");

                    b.Property<string>("MadeIn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Margin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OnTheWayInventory")
                        .HasColumnType("int");

                    b.Property<int>("Outcome")
                        .HasColumnType("int");

                    b.Property<decimal?>("RetailPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SKU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SaleEndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("SaleStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StyleNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UPC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VendorCode")
                        .HasColumnType("int");

                    b.Property<decimal?>("WholesalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Hst")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PSt")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.PurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Account")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ApproveAsnUser_fk")
                        .HasColumnType("int");

                    b.Property<int?>("ApproveGrnUser_fk")
                        .HasColumnType("int");

                    b.Property<int?>("ApprovePoUser_fk")
                        .HasColumnType("int");

                    b.Property<DateTime?>("AsnDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("AsnTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Asnumber")
                        .HasColumnType("int");

                    b.Property<string>("Associate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CancelDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreateOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<bool?>("CreatedAsn")
                        .HasColumnType("bit");

                    b.Property<bool?>("CreatedGrn")
                        .HasColumnType("bit");

                    b.Property<bool?>("CreatedPO")
                        .HasColumnType("bit");

                    b.Property<decimal?>("CustomsDuty")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DiscountDollers")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DiscountPercent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FormSO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Forwarding")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Freight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("FromWarehouse_fk")
                        .HasColumnType("int");

                    b.Property<DateTime?>("GrnDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("GrnTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Grnumber")
                        .HasColumnType("int");

                    b.Property<decimal?>("Handling")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Insurance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ItemsAsnCount")
                        .HasColumnType("int");

                    b.Property<int?>("ItemsGrnCount")
                        .HasColumnType("int");

                    b.Property<int?>("ItemsPoCount")
                        .HasColumnType("int");

                    b.Property<decimal?>("LandTransport")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("LastEditDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Others")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Percent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PoNumber")
                        .HasColumnType("int");

                    b.Property<string>("PoTerms")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PoType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<DateTime?>("ShipDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ToWarehouse_fk")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalCharges")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Vendor_fk")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApproveAsnUser_fk");

                    b.HasIndex("ApproveGrnUser_fk");

                    b.HasIndex("ApprovePoUser_fk");

                    b.HasIndex("FromWarehouse_fk");

                    b.HasIndex("ToWarehouse_fk");

                    b.HasIndex("Vendor_fk");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.SaleOrder", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Cashier_fk")
                        .HasColumnType("int");

                    b.Property<int>("Customer_fk")
                        .HasColumnType("int");

                    b.Property<byte>("Doscount")
                        .HasColumnType("tinyint");

                    b.Property<int>("FeeName")
                        .HasColumnType("int");

                    b.Property<DateTime?>("OrderedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SalesOrderNumber")
                        .HasColumnType("int");

                    b.Property<byte>("Shipping")
                        .HasColumnType("tinyint");

                    b.Property<decimal?>("SoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Tax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaxArea_fk")
                        .HasColumnType("int");

                    b.Property<bool>("Type")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("SaleOrders");
                });

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.User", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsCashier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsVisitor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lavel")
                        .HasColumnType("int");

                    b.Property<string>("NickName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("0");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("int");

                    b.Property<string>("Acountsharp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Info1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Info2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LeadTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnName("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentTerms")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TradeDiscountPercent")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.Item", b =>
                {
                    b.HasOne("Vanme_Pro.Models.DomainModels.PurchaseOrder", "PurchaseOrder")
                        .WithMany("Items")
                        .HasForeignKey("Po_fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vanme_Pro.Models.DomainModels.ProductMaster", "ProductMaster")
                        .WithMany("Items")
                        .HasForeignKey("ProductMaster_fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.ProductInventoryWarehouse", b =>
                {
                    b.HasOne("Vanme_Pro.Models.DomainModels.ProductMaster", "ProductMaster")
                        .WithMany("ProductInventoryWarehouses")
                        .HasForeignKey("ProductMaster_fk");

                    b.HasOne("Vanme_Pro.Models.DomainModels.Warehouse", "Warehouse")
                        .WithMany("ProductInventoryWarehouses")
                        .HasForeignKey("Warehouse_fk");
                });

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.PurchaseOrder", b =>
                {
                    b.HasOne("Vanme_Pro.Models.DomainModels.User", "UserCreateAsn")
                        .WithMany("CreateAsn")
                        .HasForeignKey("ApproveAsnUser_fk");

                    b.HasOne("Vanme_Pro.Models.DomainModels.User", "UserCreateGrn")
                        .WithMany("CreateGrn")
                        .HasForeignKey("ApproveGrnUser_fk");

                    b.HasOne("Vanme_Pro.Models.DomainModels.User", "UserCreatePo")
                        .WithMany("CreatePo")
                        .HasForeignKey("ApprovePoUser_fk");

                    b.HasOne("Vanme_Pro.Models.DomainModels.Warehouse", "FromWarehouse")
                        .WithMany("POFromWarehouse")
                        .HasForeignKey("FromWarehouse_fk");

                    b.HasOne("Vanme_Pro.Models.DomainModels.Warehouse", "ToWarehouse")
                        .WithMany("PoToWareHose")
                        .HasForeignKey("ToWarehouse_fk");

                    b.HasOne("Vanme_Pro.Models.DomainModels.Vendor", "Vendor")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("Vendor_fk");
                });
#pragma warning restore 612, 618
        }
    }
}
