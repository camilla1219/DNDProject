﻿// <auto-generated />
using DNDBlazorApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DNDBlazorApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241206052416_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("DNDBlazorApp.Models.Entities.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("user_account");
                });
#pragma warning restore 612, 618
        }
    }
}