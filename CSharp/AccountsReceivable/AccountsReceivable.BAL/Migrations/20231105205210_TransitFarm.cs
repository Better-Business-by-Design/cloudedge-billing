using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsReceivable.BAL.Migrations
{
    public partial class TransitFarm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FarmCostCentre",
                schema: "application",
                table: "Transit",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlantName",
                schema: "application",
                table: "Transit",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FarmStoreId",
                schema: "application",
                table: "Plant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transit_FarmCostCentre",
                schema: "application",
                table: "Transit",
                column: "FarmCostCentre");

            migrationBuilder.CreateIndex(
                name: "IX_Transit_PlantName",
                schema: "application",
                table: "Transit",
                column: "PlantName");

            migrationBuilder.AddForeignKey(
                name: "FK_Transit_Farm_FarmCostCentre",
                schema: "application",
                table: "Transit",
                column: "FarmCostCentre",
                principalSchema: "application",
                principalTable: "Farm",
                principalColumn: "CostCentre");

            migrationBuilder.AddForeignKey(
                name: "FK_Transit_Plant_PlantName",
                schema: "application",
                table: "Transit",
                column: "PlantName",
                principalSchema: "application",
                principalTable: "Plant",
                principalColumn: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transit_Farm_FarmCostCentre",
                schema: "application",
                table: "Transit");

            migrationBuilder.DropForeignKey(
                name: "FK_Transit_Plant_PlantName",
                schema: "application",
                table: "Transit");

            migrationBuilder.DropIndex(
                name: "IX_Transit_FarmCostCentre",
                schema: "application",
                table: "Transit");

            migrationBuilder.DropIndex(
                name: "IX_Transit_PlantName",
                schema: "application",
                table: "Transit");

            migrationBuilder.DropColumn(
                name: "FarmCostCentre",
                schema: "application",
                table: "Transit");

            migrationBuilder.DropColumn(
                name: "PlantName",
                schema: "application",
                table: "Transit");

            migrationBuilder.DropColumn(
                name: "FarmStoreId",
                schema: "application",
                table: "Plant");
        }
    }
}
