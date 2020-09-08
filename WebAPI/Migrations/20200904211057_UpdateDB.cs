using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoFolioAPI.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
               name: "UserId",
               table: "Wallet",
               nullable: false,
               defaultValue: 0);

            migrationBuilder.Sql("UPDATE w SET w.UserId = u.UserId FROM Wallet w INNER JOIN users u ON u.WalletId = w.WalletId");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Wallet_WalletId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletCoin_Coin_CoinId",
                table: "WalletCoin");

            migrationBuilder.DropIndex(
                name: "IX_Users_WalletId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "CoinId",
                table: "WalletCoin",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_Users_UserId",
                table: "Wallet",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletCoin_Coin_CoinId",
                table: "WalletCoin",
                column: "CoinId",
                principalTable: "Coin",
                principalColumn: "CoinId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Users_UserId",
                table: "Wallet");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletCoin_Coin_CoinId",
                table: "WalletCoin");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_UserId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Wallet");

            migrationBuilder.AlterColumn<int>(
                name: "CoinId",
                table: "WalletCoin",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_WalletId",
                table: "Users",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Wallet_WalletId",
                table: "Users",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletCoin_Coin_CoinId",
                table: "WalletCoin",
                column: "CoinId",
                principalTable: "Coin",
                principalColumn: "CoinId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
