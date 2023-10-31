using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsReceivable.BAL.Migrations
{
    public partial class DocumentChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Validation_CalcValidationId",
                schema: "application",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Validation_TransitValidationId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropTable(
                name: "DeductionDto",
                schema: "source");

            migrationBuilder.DropTable(
                name: "TransitDto",
                schema: "source");

            migrationBuilder.DropIndex(
                name: "IX_Document_TransitValidationId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "SupplierComments",
                schema: "application",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "TransitValidationId",
                schema: "application",
                table: "Document");

            migrationBuilder.RenameColumn(
                name: "StockCount",
                schema: "application",
                table: "Document",
                newName: "TransitQuantity");

            migrationBuilder.RenameColumn(
                name: "CalcValidationId",
                schema: "application",
                table: "Animal",
                newName: "ValidationId");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_CalcValidationId",
                schema: "application",
                table: "Animal",
                newName: "IX_Animal_ValidationId");

            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                schema: "application",
                table: "Document",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Comment",
                schema: "application",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserEmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "application",
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_User_UserEmailAddress",
                        column: x => x.UserEmailAddress,
                        principalSchema: "account",
                        principalTable: "User",
                        principalColumn: "EmailAddress",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transit",
                schema: "application",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transit_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "application",
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_DocumentId",
                schema: "application",
                table: "Comment",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserEmailAddress",
                schema: "application",
                table: "Comment",
                column: "UserEmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Transit_DocumentId",
                schema: "application",
                table: "Transit",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Validation_ValidationId",
                schema: "application",
                table: "Animal",
                column: "ValidationId",
                principalSchema: "enum",
                principalTable: "Validation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Validation_ValidationId",
                schema: "application",
                table: "Animal");

            migrationBuilder.DropTable(
                name: "Comment",
                schema: "application");

            migrationBuilder.DropTable(
                name: "Transit",
                schema: "application");

            migrationBuilder.DropColumn(
                name: "StockQuantity",
                schema: "application",
                table: "Document");

            migrationBuilder.EnsureSchema(
                name: "source");

            migrationBuilder.RenameColumn(
                name: "TransitQuantity",
                schema: "application",
                table: "Document",
                newName: "StockCount");

            migrationBuilder.RenameColumn(
                name: "ValidationId",
                schema: "application",
                table: "Animal",
                newName: "CalcValidationId");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_ValidationId",
                schema: "application",
                table: "Animal",
                newName: "IX_Animal_CalcValidationId");

            migrationBuilder.AddColumn<string>(
                name: "SupplierComments",
                schema: "application",
                table: "Document",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "TransitValidationId",
                schema: "application",
                table: "Document",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "DeductionDto",
                schema: "source",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Meatworks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductionDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransitDto",
                schema: "source",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostCentre = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitDto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_TransitValidationId",
                schema: "application",
                table: "Document",
                column: "TransitValidationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Validation_CalcValidationId",
                schema: "application",
                table: "Animal",
                column: "CalcValidationId",
                principalSchema: "enum",
                principalTable: "Validation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Validation_TransitValidationId",
                schema: "application",
                table: "Document",
                column: "TransitValidationId",
                principalSchema: "enum",
                principalTable: "Validation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
