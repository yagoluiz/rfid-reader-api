﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Asset.API.Infrastructure.Migrations
{
    [DbContext(typeof(AssetDbContext))]
    [Migration("20190306005836_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Asset.API.Infrastructure.Entities.AssetItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssetLocalId");

                    b.Property<int>("AssetTypeId");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Epc")
                        .IsRequired()
                        .HasMaxLength(24);

                    b.Property<string>("IdentifierNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("Situation");

                    b.HasKey("Id");

                    b.HasIndex("AssetLocalId");

                    b.HasIndex("AssetTypeId");

                    b.ToTable("AssetItem");
                });

            modelBuilder.Entity("Asset.API.Infrastructure.Entities.AssetLocal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(250);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("AssetLocal");
                });

            modelBuilder.Entity("Asset.API.Infrastructure.Entities.AssetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("AssetType");
                });

            modelBuilder.Entity("Asset.API.Infrastructure.Entities.AssetItem", b =>
                {
                    b.HasOne("Asset.API.Infrastructure.Entities.AssetLocal", "Local")
                        .WithMany()
                        .HasForeignKey("AssetLocalId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Asset.API.Infrastructure.Entities.AssetType", "Type")
                        .WithMany()
                        .HasForeignKey("AssetTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
