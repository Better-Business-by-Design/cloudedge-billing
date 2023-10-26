using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsReceivable.BAL.Migrations
{
    public partial class CalculateFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalcGrossCostTotal",
                schema: "application",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "CalcGstCostTotal",
                schema: "application",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "CalcStockTotal",
                schema: "application",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "CalcStockWeightTotal",
                schema: "application",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "GrossCostTotal",
                schema: "application",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "GstCostTotal",
                schema: "application",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "StockTotal",
                schema: "application",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "CalcGrossCost",
                schema: "application",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "CalcGstCost",
                schema: "application",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "CalcStockWeight",
                schema: "application",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "GrossCost",
                schema: "application",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "StockWeightTotal",
                schema: "application",
                table: "Document",
                newName: "WeightTotal");

            migrationBuilder.RenameColumn(
                name: "StockWeight",
                schema: "application",
                table: "Animal",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "GstCost",
                schema: "application",
                table: "Animal",
                newName: "CalcPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeightTotal",
                schema: "application",
                table: "Document",
                newName: "StockWeightTotal");

            migrationBuilder.RenameColumn(
                name: "Weight",
                schema: "application",
                table: "Animal",
                newName: "StockWeight");

            migrationBuilder.RenameColumn(
                name: "CalcPrice",
                schema: "application",
                table: "Animal",
                newName: "GstCost");

            migrationBuilder.AddColumn<decimal>(
                name: "CalcGrossCostTotal",
                schema: "application",
                table: "Document",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CalcGstCostTotal",
                schema: "application",
                table: "Document",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CalcStockTotal",
                schema: "application",
                table: "Document",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CalcStockWeightTotal",
                schema: "application",
                table: "Document",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GrossCostTotal",
                schema: "application",
                table: "Document",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GstCostTotal",
                schema: "application",
                table: "Document",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StockTotal",
                schema: "application",
                table: "Document",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CalcGrossCost",
                schema: "application",
                table: "Animal",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CalcGstCost",
                schema: "application",
                table: "Animal",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CalcStockWeight",
                schema: "application",
                table: "Animal",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GrossCost",
                schema: "application",
                table: "Animal",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
