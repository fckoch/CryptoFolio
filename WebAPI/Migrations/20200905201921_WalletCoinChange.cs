using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoFolioAPI.Migrations
{
    public partial class WalletCoinChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Users_UserId",
                table: "Wallet");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletCoin_Coin_CoinId",
                table: "WalletCoin");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletCoin_Wallet_WalletId",
                table: "WalletCoin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletCoin",
                table: "WalletCoin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet");

            migrationBuilder.RenameTable(
                name: "WalletCoin",
                newName: "WalletCoins");

            migrationBuilder.RenameTable(
                name: "Wallet",
                newName: "Wallets");

            migrationBuilder.RenameIndex(
                name: "IX_WalletCoin_WalletId",
                table: "WalletCoins",
                newName: "IX_WalletCoins_WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_WalletCoin_CoinId",
                table: "WalletCoins",
                newName: "IX_WalletCoins_CoinId");

            migrationBuilder.RenameIndex(
                name: "IX_Wallet_UserId",
                table: "Wallets",
                newName: "IX_Wallets_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "WalletId",
                table: "WalletCoins",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletCoins",
                table: "WalletCoins",
                column: "WalletCoinId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletCoins_Coin_CoinId",
                table: "WalletCoins",
                column: "CoinId",
                principalTable: "Coin",
                principalColumn: "CoinId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletCoins_Wallets_WalletId",
                table: "WalletCoins",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletCoins_Coin_CoinId",
                table: "WalletCoins");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletCoins_Wallets_WalletId",
                table: "WalletCoins");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Users_UserId",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletCoins",
                table: "WalletCoins");

            migrationBuilder.RenameTable(
                name: "Wallets",
                newName: "Wallet");

            migrationBuilder.RenameTable(
                name: "WalletCoins",
                newName: "WalletCoin");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_UserId",
                table: "Wallet",
                newName: "IX_Wallet_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WalletCoins_WalletId",
                table: "WalletCoin",
                newName: "IX_WalletCoin_WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_WalletCoins_CoinId",
                table: "WalletCoin",
                newName: "IX_WalletCoin_CoinId");

            migrationBuilder.AlterColumn<int>(
                name: "WalletId",
                table: "WalletCoin",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet",
                column: "WalletId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletCoin",
                table: "WalletCoin",
                column: "WalletCoinId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_WalletCoin_Wallet_WalletId",
                table: "WalletCoin",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
