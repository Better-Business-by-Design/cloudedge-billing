using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsReceivable.BAL.Migrations
{
    public partial class NoneEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)0,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "None", "None" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "None");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Role",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "None");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "SpeciesType",
                keyColumn: "Id",
                keyValue: (byte)0,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "None", "NONE" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "None");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Validation",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "None");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)0,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Missing", "MISSING" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "Missing");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Role",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "Missing");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "SpeciesType",
                keyColumn: "Id",
                keyValue: (byte)0,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Missing", "MISSING" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "Missing");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Validation",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "Missing");
        }
    }
}
