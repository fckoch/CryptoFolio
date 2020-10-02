using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoFolioWorkerService.Migrations
{
    public partial class userupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coin",
                columns: table => new
                {
                    CoinId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoinName = table.Column<string>(nullable: true),
                    CurrentValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Symbol = table.Column<string>(nullable: true),
                    PriceChange = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PriceChangePct = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    MarketCap = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    AllTimeHigh = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coin", x => x.CoinId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coin");
        }
    }
}
