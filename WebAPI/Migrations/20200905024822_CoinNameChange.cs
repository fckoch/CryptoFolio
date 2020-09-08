using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoFolioAPI.Migrations
{
    public partial class CoinNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Coin");

            migrationBuilder.AddColumn<string>(
                name: "CoinName",
                table: "Coin",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoinName",
                table: "Coin");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Coin",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
