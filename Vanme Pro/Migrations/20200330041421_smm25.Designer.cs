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
    [Migration("20200330041421_smm25")]
    partial class smm25
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.Item", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<decimal>("AsnItemsPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AsnPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("AsnQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("GrnQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Note")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Note :  ");

                    b.Property<decimal>("PoItemsPrice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

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

                    b.HasKey("Id");

                    b.HasIndex("Po_fk");

                    b.HasIndex("ProductMaster_fk");

                    b.ToTable("Items");
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

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Income")
                        .HasColumnType("int");

                    b.Property<int>("Inventory")
                        .HasColumnType("int");

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

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SKU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StyleNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UPC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VendorCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.PurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Account")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("AsnDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("AsnTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("Asnumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Associate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CancelDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("CreatedAsn")
                        .HasColumnType("bit");

                    b.Property<bool?>("CreatedGrn")
                        .HasColumnType("bit");

                    b.Property<bool?>("CreatedPO")
                        .HasColumnType("bit");

                    b.Property<decimal?>("DiscountDollers")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("DiscountPercent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Fee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("FeeType")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FormSO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Freight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("GrnDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Grnumber")
                        .HasColumnType("bigint");

                    b.Property<int?>("ItemsAsnCount")
                        .HasColumnType("int");

                    b.Property<int?>("ItemsGrnCount")
                        .HasColumnType("int");

                    b.Property<int?>("ItemsPoCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastEditDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("PoNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("PoTerms")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PoType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ShipDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShipToStore")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Vendor_fk")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Vendor_fk");

                    b.ToTable("PurchaseOrders");
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

            modelBuilder.Entity("Vanme_Pro.Models.DomainModels.PurchaseOrder", b =>
                {
                    b.HasOne("Vanme_Pro.Models.DomainModels.Vendor", "Vendor")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("Vendor_fk");
                });
#pragma warning restore 612, 618
        }
    }
}
