using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsReceivable.BAL.Migrations
{
    public partial class EnumCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Grade_GradeId",
                schema: "application",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalType_SpeciesType_SpeciesTypeId",
                schema: "enum",
                table: "AnimalType");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalTypeSummary_AnimalType_AnimalTypeId",
                schema: "application",
                table: "AnimalTypeSummary");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_SpeciesType_SpeciesTypeId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Status_StatusId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_TransitDto_TransitId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_AnimalType_AnimalTypeId",
                schema: "enum",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Price_Grade_GradeId",
                schema: "application",
                table: "Price");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Status_StatusId",
                schema: "application",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Uplift_AnimalType_AnimalTypeId",
                schema: "application",
                table: "Uplift");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                schema: "account",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Document_TransitId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "TransitId",
                schema: "application",
                table: "Document");

            migrationBuilder.AlterColumn<byte>(
                name: "SpeciesTypeId",
                schema: "application",
                table: "Document",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "CalcValidationId",
                schema: "application",
                table: "Document",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "TransitValidationId",
                schema: "application",
                table: "Document",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "CalcValidationId",
                schema: "application",
                table: "Animal",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_Document_CalcValidationId",
                schema: "application",
                table: "Document",
                column: "CalcValidationId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_TransitValidationId",
                schema: "application",
                table: "Document",
                column: "TransitValidationId");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_CalcValidationId",
                schema: "application",
                table: "Animal",
                column: "CalcValidationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Grade_GradeId",
                schema: "application",
                table: "Animal",
                column: "GradeId",
                principalSchema: "enum",
                principalTable: "Grade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_AnimalType_SpeciesType_SpeciesTypeId",
                schema: "enum",
                table: "AnimalType",
                column: "SpeciesTypeId",
                principalSchema: "enum",
                principalTable: "SpeciesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalTypeSummary_AnimalType_AnimalTypeId",
                schema: "application",
                table: "AnimalTypeSummary",
                column: "AnimalTypeId",
                principalSchema: "enum",
                principalTable: "AnimalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_SpeciesType_SpeciesTypeId",
                schema: "application",
                table: "Document",
                column: "SpeciesTypeId",
                principalSchema: "enum",
                principalTable: "SpeciesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Status_StatusId",
                schema: "application",
                table: "Document",
                column: "StatusId",
                principalSchema: "enum",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Validation_CalcValidationId",
                schema: "application",
                table: "Document",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_AnimalType_AnimalTypeId",
                schema: "enum",
                table: "Grade",
                column: "AnimalTypeId",
                principalSchema: "enum",
                principalTable: "AnimalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Grade_GradeId",
                schema: "application",
                table: "Price",
                column: "GradeId",
                principalSchema: "enum",
                principalTable: "Grade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Status_StatusId",
                schema: "application",
                table: "Schedule",
                column: "StatusId",
                principalSchema: "enum",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Uplift_AnimalType_AnimalTypeId",
                schema: "application",
                table: "Uplift",
                column: "AnimalTypeId",
                principalSchema: "enum",
                principalTable: "AnimalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                schema: "account",
                table: "User",
                column: "RoleId",
                principalSchema: "enum",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Grade_GradeId",
                schema: "application",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Validation_CalcValidationId",
                schema: "application",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalType_SpeciesType_SpeciesTypeId",
                schema: "enum",
                table: "AnimalType");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalTypeSummary_AnimalType_AnimalTypeId",
                schema: "application",
                table: "AnimalTypeSummary");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_SpeciesType_SpeciesTypeId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Status_StatusId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Validation_CalcValidationId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Validation_TransitValidationId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_AnimalType_AnimalTypeId",
                schema: "enum",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Price_Grade_GradeId",
                schema: "application",
                table: "Price");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Status_StatusId",
                schema: "application",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Uplift_AnimalType_AnimalTypeId",
                schema: "application",
                table: "Uplift");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                schema: "account",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Document_CalcValidationId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_TransitValidationId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Animal_CalcValidationId",
                schema: "application",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "CalcValidationId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "TransitValidationId",
                schema: "application",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "CalcValidationId",
                schema: "application",
                table: "Animal");

            migrationBuilder.AlterColumn<byte>(
                name: "SpeciesTypeId",
                schema: "application",
                table: "Document",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<int>(
                name: "TransitId",
                schema: "application",
                table: "Document",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_TransitId",
                schema: "application",
                table: "Document",
                column: "TransitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Grade_GradeId",
                schema: "application",
                table: "Animal",
                column: "GradeId",
                principalSchema: "enum",
                principalTable: "Grade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalType_SpeciesType_SpeciesTypeId",
                schema: "enum",
                table: "AnimalType",
                column: "SpeciesTypeId",
                principalSchema: "enum",
                principalTable: "SpeciesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalTypeSummary_AnimalType_AnimalTypeId",
                schema: "application",
                table: "AnimalTypeSummary",
                column: "AnimalTypeId",
                principalSchema: "enum",
                principalTable: "AnimalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_SpeciesType_SpeciesTypeId",
                schema: "application",
                table: "Document",
                column: "SpeciesTypeId",
                principalSchema: "enum",
                principalTable: "SpeciesType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Status_StatusId",
                schema: "application",
                table: "Document",
                column: "StatusId",
                principalSchema: "enum",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_TransitDto_TransitId",
                schema: "application",
                table: "Document",
                column: "TransitId",
                principalSchema: "source",
                principalTable: "TransitDto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_AnimalType_AnimalTypeId",
                schema: "enum",
                table: "Grade",
                column: "AnimalTypeId",
                principalSchema: "enum",
                principalTable: "AnimalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Grade_GradeId",
                schema: "application",
                table: "Price",
                column: "GradeId",
                principalSchema: "enum",
                principalTable: "Grade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Status_StatusId",
                schema: "application",
                table: "Schedule",
                column: "StatusId",
                principalSchema: "enum",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Uplift_AnimalType_AnimalTypeId",
                schema: "application",
                table: "Uplift",
                column: "AnimalTypeId",
                principalSchema: "enum",
                principalTable: "AnimalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                schema: "account",
                table: "User",
                column: "RoleId",
                principalSchema: "enum",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
