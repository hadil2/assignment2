﻿// <auto-generated />
using System;
using Lab4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lab4.Migrations
{
    [DbContext(typeof(MarketDbContext))]
    [Migration("20221209122616_second")]
    partial class second
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lab4.Models.Advertisement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("BrokerageID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BrokerageID");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("Lab4.Models.Brokerage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Fee")
                        .HasColumnType("money");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Brokerage", (string)null);
                });

            modelBuilder.Entity("Lab4.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("Lab4.Models.Subscription", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("BrokerageId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ClientId", "BrokerageId");

                    b.HasIndex("BrokerageId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Lab4.Models.Advertisement", b =>
                {
                    b.HasOne("Lab4.Models.Brokerage", "Brokerage")
                        .WithMany("advertisements")
                        .HasForeignKey("BrokerageID");

                    b.Navigation("Brokerage");
                });

            modelBuilder.Entity("Lab4.Models.Subscription", b =>
                {
                    b.HasOne("Lab4.Models.Brokerage", null)
                        .WithMany("subscriptions")
                        .HasForeignKey("BrokerageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lab4.Models.Client", null)
                        .WithMany("subscriptions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lab4.Models.Brokerage", b =>
                {
                    b.Navigation("advertisements");

                    b.Navigation("subscriptions");
                });

            modelBuilder.Entity("Lab4.Models.Client", b =>
                {
                    b.Navigation("subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
