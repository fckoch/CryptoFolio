using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoFolioAPI.Migrations
{
    public partial class Droprankcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Coin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Coin",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
