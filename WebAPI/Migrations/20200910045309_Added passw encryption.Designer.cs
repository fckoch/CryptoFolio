﻿// <auto-generated />
using System;
using CryptoFolioAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CryptoFolioAPI.Migrations
{
    [DbContext(typeof(CryptoFolioContext))]
    [Migration("20200910045309_Added passw encryption")]
    partial class Addedpasswencryption
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CryptoFolioAPI.Models.Entities.Coin", b =>
                {
                    b.Property<int>("CoinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoinName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CurrentValue")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("CoinId");

                    b.ToTable("Coin");
                });

            modelBuilder.Entity("CryptoFolioAPI.Models.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CryptoFolioAPI.Models.Entities.Wallet", b =>
                {
                    b.Property<int>("WalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("WalletId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("CryptoFolioAPI.Models.Entities.WalletCoin", b =>
                {
                    b.Property<int>("WalletCoinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BuyDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CoinId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValueWhenBought")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("WalletCoinId");

                    b.HasIndex("CoinId");

                    b.HasIndex("WalletId");

                    b.ToTable("WalletCoins");
                });

            modelBuilder.Entity("CryptoFolioAPI.Models.Entities.Wallet", b =>
                {
                    b.HasOne("CryptoFolioAPI.Models.Entities.User", null)
                        .WithOne("Wallet")
                        .HasForeignKey("CryptoFolioAPI.Models.Entities.Wallet", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CryptoFolioAPI.Models.Entities.WalletCoin", b =>
                {
                    b.HasOne("CryptoFolioAPI.Models.Entities.Coin", "Coin")
                        .WithMany()
                        .HasForeignKey("CoinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CryptoFolioAPI.Models.Entities.Wallet", null)
                        .WithMany("Walletcoins")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
