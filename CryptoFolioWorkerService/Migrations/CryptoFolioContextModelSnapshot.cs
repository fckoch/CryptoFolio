﻿// <auto-generated />
using CryptoFolioWorkerService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CryptoFolioWorkerService.Migrations
{
    [DbContext(typeof(CryptoFolioContext))]
    partial class CryptoFolioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CryptoFolioWorkerService.Coin", b =>
                {
                    b.Property<int>("CoinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AllTimeHigh")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("CoinName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CurrentValue")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("MarketCap")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PriceChange")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PriceChangePct")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CoinId");

                    b.ToTable("Coin");
                });
#pragma warning restore 612, 618
        }
    }
}