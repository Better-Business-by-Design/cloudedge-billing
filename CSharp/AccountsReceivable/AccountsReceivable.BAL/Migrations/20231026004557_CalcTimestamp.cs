using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsReceivable.BAL.Migrations
{
    public partial class CalcTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CalcTimestamp",
                schema: "application",
                table: "Document",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { (byte)3, "Missing" });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[] { (byte)5, "Missing" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "enum",
                table: "Role",
                keyColumn: "Id",
                keyValue: (byte)3);

            migrationBuilder.DeleteData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)5);

            migrationBuilder.DropColumn(
                name: "CalcTimestamp",
                schema: "application",
                table: "Document");
        }
    }
}
