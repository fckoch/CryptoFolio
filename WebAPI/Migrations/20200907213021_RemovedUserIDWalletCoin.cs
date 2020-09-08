using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoFolioAPI.Migrations
{
    public partial class RemovedUserIDWalletCoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WalletCoins");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "WalletCoins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
