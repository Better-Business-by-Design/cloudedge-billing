using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudEdgeBilling.BAL.Migrations
{
    public partial class PriceModifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modifier",
                schema: "application",
                table: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Modifier",
                schema: "application",
                table: "Price",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
