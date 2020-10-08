using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoFolioAPI.Migrations
{
    public partial class addedamounttowalletcoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "WalletCoins",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "WalletCoins");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
