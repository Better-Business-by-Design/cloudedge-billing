using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsReceivable.BAL.Migrations
{
    public partial class transit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "SpeciesTypeId",
                schema: "application",
                table: "Transit",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_Transit_SpeciesTypeId",
                schema: "application",
                table: "Transit",
                column: "SpeciesTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transit_SpeciesType_SpeciesTypeId",
                schema: "application",
                table: "Transit",
                column: "SpeciesTypeId",
                principalSchema: "enum",
                principalTable: "SpeciesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transit_SpeciesType_SpeciesTypeId",
                schema: "application",
                table: "Transit");

            migrationBuilder.DropIndex(
                name: "IX_Transit_SpeciesTypeId",
                schema: "application",
                table: "Transit");

            migrationBuilder.DropColumn(
                name: "SpeciesTypeId",
                schema: "application",
                table: "Transit");
        }
    }
}
