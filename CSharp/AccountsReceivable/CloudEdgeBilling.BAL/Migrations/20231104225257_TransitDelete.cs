using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsReceivable.BAL.Migrations
{
    public partial class TransitDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transit_Document_DocumentId",
                schema: "application",
                table: "Transit");

            migrationBuilder.DropForeignKey(
                name: "FK_Transit_SpeciesType_SpeciesTypeId",
                schema: "application",
                table: "Transit");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentId",
                schema: "application",
                table: "Transit",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Transit_Document_DocumentId",
                schema: "application",
                table: "Transit",
                column: "DocumentId",
                principalSchema: "application",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Transit_SpeciesType_SpeciesTypeId",
                schema: "application",
                table: "Transit",
                column: "SpeciesTypeId",
                principalSchema: "enum",
                principalTable: "SpeciesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transit_Document_DocumentId",
                schema: "application",
                table: "Transit");

            migrationBuilder.DropForeignKey(
                name: "FK_Transit_SpeciesType_SpeciesTypeId",
                schema: "application",
                table: "Transit");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentId",
                schema: "application",
                table: "Transit",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transit_Document_DocumentId",
                schema: "application",
                table: "Transit",
                column: "DocumentId",
                principalSchema: "application",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
