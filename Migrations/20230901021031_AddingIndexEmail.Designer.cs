﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using quejapp.Data;
using s10.Back.Data;

#nullable disable

namespace s10.Migrations
{
    [DbContext(typeof(RedCoContext))]
    [Migration("20230901021031_AddingIndexEmail")]
    partial class AddingIndexEmail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("quejapp.Models.AppUser", b =>
                {
                    b.Property<int>("User_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_ID"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("User_ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("AppUser");
                });

            modelBuilder.Entity("quejapp.Models.Category", b =>
                {
                    b.Property<int>("Category_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Category_ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Category_ID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("quejapp.Models.Comment", b =>
                {
                    b.Property<int>("Comment_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Comment_ID"), 1L, 1);

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Complaint_ID")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("Comment_ID");

                    b.HasIndex("Complaint_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("quejapp.Models.District", b =>
                {
                    b.Property<int>("District_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("District_ID"), 1L, 1);

                    b.Property<int>("Locality_ID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("District_ID");

                    b.HasIndex("Locality_ID");

                    b.ToTable("District");
                });

            modelBuilder.Entity("quejapp.Models.Locality", b =>
                {
                    b.Property<int>("Locality_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Locality_ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Locality_ID");

                    b.ToTable("Locality");
                });

            modelBuilder.Entity("quejapp.Models.Queja", b =>
                {
                    b.Property<int>("Complaint_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Complaint_ID"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Category_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("District_ID")
                        .HasColumnType("int");

                    b.Property<string>("PhotoAdress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("User_ID")
                        .HasColumnType("int");

                    b.Property<string>("VideoAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Complaint_ID");

                    b.HasIndex("Category_ID");

                    b.HasIndex("District_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("Queja");
                });

            modelBuilder.Entity("quejapp.Models.Comment", b =>
                {
                    b.HasOne("quejapp.Models.Queja", "Complaint")
                        .WithMany("Comments")
                        .HasForeignKey("Complaint_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("quejapp.Models.AppUser", "User")
                        .WithMany("Comments")
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Complaint");

                    b.Navigation("User");
                });

            modelBuilder.Entity("quejapp.Models.District", b =>
                {
                    b.HasOne("quejapp.Models.Locality", "Locality")
                        .WithMany("Districts")
                        .HasForeignKey("Locality_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Locality");
                });

            modelBuilder.Entity("quejapp.Models.Queja", b =>
                {
                    b.HasOne("quejapp.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("Category_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("quejapp.Models.District", "District")
                        .WithMany()
                        .HasForeignKey("District_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("quejapp.Models.AppUser", "User")
                        .WithMany("Complaints")
                        .HasForeignKey("User_ID");

                    b.Navigation("Category");

                    b.Navigation("District");

                    b.Navigation("User");
                });

            modelBuilder.Entity("quejapp.Models.AppUser", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Complaints");
                });

            modelBuilder.Entity("quejapp.Models.Locality", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("quejapp.Models.Queja", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
