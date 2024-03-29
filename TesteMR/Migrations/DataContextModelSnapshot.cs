﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TesteMR.Data;

namespace TesteMR.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TesteMR.Models.Artigo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("nId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("cCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("cDescription")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("cUnitCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime?>("dChangedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dCreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("nUnitPrice")
                        .HasColumnType("decimal(20, 8)");

                    b.Property<Guid>("uId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Artigos");
                });

            modelBuilder.Entity("TesteMR.Models.Unidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("nId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("cCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("cDescription")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<DateTime?>("dChangedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dCreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("uId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Unidades");
                });

            modelBuilder.Entity("TesteMR.Models.UnidadeArtigo", b =>
                {
                    b.Property<int>("nId")
                        .HasColumnType("int");

                    b.Property<int>("ArtigoId")
                        .HasColumnName("uProductId")
                        .HasColumnType("int");

                    b.Property<int>("UnidadeId")
                        .HasColumnName("uUnidadeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("dChangedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dCreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("nBaseUnitQty")
                        .HasColumnType("decimal(20, 10)");

                    b.Property<decimal>("nPrice")
                        .HasColumnType("decimal(20, 8)");

                    b.Property<Guid>("uId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("nId");

                    b.ToTable("UnidadesArtigos");
                });

            modelBuilder.Entity("TesteMR.Models.UnidadeArtigo", b =>
                {
                    b.HasOne("TesteMR.Models.Artigo", "Artigo")
                        .WithMany("UnidadesArtigos")
                        .HasForeignKey("nId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TesteMR.Models.Unidade", "Unidade")
                        .WithMany()
                        .HasForeignKey("nId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
