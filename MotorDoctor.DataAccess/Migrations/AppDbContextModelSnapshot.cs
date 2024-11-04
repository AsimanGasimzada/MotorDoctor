﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MotorDoctor.DataAccess.Contexts;

#nullable disable

namespace MotorDoctor.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MotorDoctor.Core.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.BrandDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("LanguageId", "BrandId")
                        .IsUnique();

                    b.ToTable("BrandDetails");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.CategoryDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LanguageId", "CategoryId")
                        .IsUnique();

                    b.ToTable("CategoryDetails");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "AZE",
                            ImagePath = "https://res.cloudinary.com/dlilcwizx/image/upload/v1730241623/motordoctor.az/fajaznl6ilmlbmo05xbw.png",
                            Name = "Azerbaijan"
                        },
                        new
                        {
                            Id = 2,
                            Code = "ENG",
                            ImagePath = "https://res.cloudinary.com/dlilcwizx/image/upload/v1730241623/motordoctor.az/mygg6rnd9rkxwc6vlljx.png",
                            Name = "English"
                        },
                        new
                        {
                            Id = 3,
                            Code = "RUS",
                            ImagePath = "https://res.cloudinary.com/dlilcwizx/image/upload/v1730241623/motordoctor.az/upkqfbyfpy7rvmjdwfsm.png",
                            Name = "Russian"
                        });
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.ProductDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ProductId", "LanguageId")
                        .IsUnique();

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsMain")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId", "IsMain")
                        .IsUnique()
                        .HasFilter("[IsMain] = 1");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.ProductSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId", "Size")
                        .IsUnique();

                    b.ToTable("ProductSizes");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("Settings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Key = "Logo"
                        },
                        new
                        {
                            Id = 2,
                            Key = "Telefon1"
                        },
                        new
                        {
                            Id = 3,
                            Key = "Telefon2"
                        },
                        new
                        {
                            Id = 4,
                            Key = "FacebookLink"
                        },
                        new
                        {
                            Id = 5,
                            Key = "InstagramLink"
                        },
                        new
                        {
                            Id = 6,
                            Key = "LinkedinLink"
                        },
                        new
                        {
                            Id = 7,
                            Key = "Unvan"
                        });
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.SettingDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<int>("SettingId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.HasKey("Id");

                    b.HasIndex("SettingId");

                    b.HasIndex("LanguageId", "SettingId")
                        .IsUnique();

                    b.ToTable("SettingDetails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LanguageId = 1,
                            SettingId = 1,
                            Value = "logo.png"
                        },
                        new
                        {
                            Id = 2,
                            LanguageId = 2,
                            SettingId = 1,
                            Value = "logo.png"
                        },
                        new
                        {
                            Id = 3,
                            LanguageId = 3,
                            SettingId = 1,
                            Value = "logo.png"
                        },
                        new
                        {
                            Id = 4,
                            LanguageId = 1,
                            SettingId = 2,
                            Value = "+994 51 434 15 23"
                        },
                        new
                        {
                            Id = 5,
                            LanguageId = 2,
                            SettingId = 2,
                            Value = "+994 51 434 15 23"
                        },
                        new
                        {
                            Id = 6,
                            LanguageId = 3,
                            SettingId = 2,
                            Value = "+994 51 434 15 23"
                        },
                        new
                        {
                            Id = 7,
                            LanguageId = 1,
                            SettingId = 3,
                            Value = "+994 51 434 15 23"
                        },
                        new
                        {
                            Id = 8,
                            LanguageId = 2,
                            SettingId = 3,
                            Value = "+994 51 434 15 23"
                        },
                        new
                        {
                            Id = 9,
                            LanguageId = 3,
                            SettingId = 3,
                            Value = "+994 51 434 15 23"
                        },
                        new
                        {
                            Id = 10,
                            LanguageId = 1,
                            SettingId = 4,
                            Value = "facebook.png"
                        },
                        new
                        {
                            Id = 11,
                            LanguageId = 2,
                            SettingId = 4,
                            Value = "facebook.com"
                        },
                        new
                        {
                            Id = 12,
                            LanguageId = 3,
                            SettingId = 4,
                            Value = "facebook.com"
                        },
                        new
                        {
                            Id = 13,
                            LanguageId = 1,
                            SettingId = 5,
                            Value = "instagramcomg"
                        },
                        new
                        {
                            Id = 14,
                            LanguageId = 2,
                            SettingId = 5,
                            Value = "instagramcomg"
                        },
                        new
                        {
                            Id = 15,
                            LanguageId = 3,
                            SettingId = 5,
                            Value = "instagramcomg"
                        },
                        new
                        {
                            Id = 16,
                            LanguageId = 1,
                            SettingId = 6,
                            Value = "linkedin.com"
                        },
                        new
                        {
                            Id = 17,
                            LanguageId = 2,
                            SettingId = 6,
                            Value = "linkedin.com"
                        },
                        new
                        {
                            Id = 18,
                            LanguageId = 3,
                            SettingId = 6,
                            Value = "linkedin.com"
                        },
                        new
                        {
                            Id = 19,
                            LanguageId = 1,
                            SettingId = 7,
                            Value = "address.com"
                        },
                        new
                        {
                            Id = 20,
                            LanguageId = 2,
                            SettingId = 7,
                            Value = "address.com"
                        },
                        new
                        {
                            Id = 21,
                            LanguageId = 3,
                            SettingId = 7,
                            Value = "addressp.com"
                        });
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.Slider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.SliderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ButtonTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<int>("SliderId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("SliderId");

                    b.ToTable("SliderDetails");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.BrandDetail", b =>
                {
                    b.HasOne("MotorDoctor.Core.Entities.Brand", "Brand")
                        .WithMany("BrandDetails")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotorDoctor.Core.Entities.Language", "Language")
                        .WithMany("BrandDetails")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.Category", b =>
                {
                    b.HasOne("MotorDoctor.Core.Entities.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.CategoryDetail", b =>
                {
                    b.HasOne("MotorDoctor.Core.Entities.Category", "Category")
                        .WithMany("CategoryDetails")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotorDoctor.Core.Entities.Language", "Language")
                        .WithMany("CategoryDetails")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.Product", b =>
                {
                    b.HasOne("MotorDoctor.Core.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotorDoctor.Core.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.ProductDetail", b =>
                {
                    b.HasOne("MotorDoctor.Core.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotorDoctor.Core.Entities.Product", "Product")
                        .WithMany("ProductDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.ProductImage", b =>
                {
                    b.HasOne("MotorDoctor.Core.Entities.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.ProductSize", b =>
                {
                    b.HasOne("MotorDoctor.Core.Entities.Product", "Product")
                        .WithMany("ProductSizes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.SettingDetail", b =>
                {
                    b.HasOne("MotorDoctor.Core.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotorDoctor.Core.Entities.Setting", "Setting")
                        .WithMany("SettingDetails")
                        .HasForeignKey("SettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Setting");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.SliderDetail", b =>
                {
                    b.HasOne("MotorDoctor.Core.Entities.Language", "Language")
                        .WithMany("SliderDetails")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotorDoctor.Core.Entities.Slider", "Slider")
                        .WithMany("SliderDetails")
                        .HasForeignKey("SliderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Slider");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.Brand", b =>
                {
                    b.Navigation("BrandDetails");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.Category", b =>
                {
                    b.Navigation("CategoryDetails");

                    b.Navigation("Children");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.Language", b =>
                {
                    b.Navigation("BrandDetails");

                    b.Navigation("CategoryDetails");

                    b.Navigation("SliderDetails");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.Product", b =>
                {
                    b.Navigation("ProductDetails");

                    b.Navigation("ProductImages");

                    b.Navigation("ProductSizes");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.Setting", b =>
                {
                    b.Navigation("SettingDetails");
                });

            modelBuilder.Entity("MotorDoctor.Core.Entities.Slider", b =>
                {
                    b.Navigation("SliderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
