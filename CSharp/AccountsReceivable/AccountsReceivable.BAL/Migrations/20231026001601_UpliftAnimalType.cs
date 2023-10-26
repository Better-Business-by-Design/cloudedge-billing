using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsReceivable.BAL.Migrations
{
    public partial class UpliftAnimalType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Uplift_Unique",
                schema: "application",
                table: "Uplift");

            migrationBuilder.CreateIndex(
                name: "Uplift_Unique",
                schema: "application",
                table: "Uplift",
                columns: new[] { "ScheduleId", "Name", "AnimalTypeId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Uplift_Unique",
                schema: "application",
                table: "Uplift");

            migrationBuilder.CreateIndex(
                name: "Uplift_Unique",
                schema: "application",
                table: "Uplift",
                columns: new[] { "ScheduleId", "Name" },
                unique: true);
        }
    }
}
