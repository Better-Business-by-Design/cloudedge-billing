using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsReceivable.BAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "application");

            migrationBuilder.EnsureSchema(
                name: "enum");

            migrationBuilder.EnsureSchema(
                name: "account");

            migrationBuilder.EnsureSchema(
                name: "source");

            migrationBuilder.CreateTable(
                name: "DeductionDto",
                schema: "source",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Meatworks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductionDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Farm",
                schema: "application",
                columns: table => new
                {
                    CostCentre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farm", x => x.CostCentre);
                });

            migrationBuilder.CreateTable(
                name: "Meatwork",
                schema: "application",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meatwork", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "enum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeciesType",
                schema: "enum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeciesType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                schema: "enum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransitDto",
                schema: "source",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CostCentre = table.Column<int>(type: "int", nullable: false),
                    PlantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnimalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Validation",
                schema: "enum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plant",
                schema: "application",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeatworkName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Plant_Meatwork_MeatworkName",
                        column: x => x.MeatworkName,
                        principalSchema: "application",
                        principalTable: "Meatwork",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                schema: "application",
                columns: table => new
                {
                    FarmCostCentre = table.Column<int>(type: "int", nullable: false),
                    MeatworkName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SupplierNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => new { x.FarmCostCentre, x.MeatworkName });
                    table.ForeignKey(
                        name: "FK_Supplier_Farm_FarmCostCentre",
                        column: x => x.FarmCostCentre,
                        principalSchema: "application",
                        principalTable: "Farm",
                        principalColumn: "CostCentre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supplier_Meatwork_MeatworkName",
                        column: x => x.MeatworkName,
                        principalSchema: "application",
                        principalTable: "Meatwork",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "account",
                columns: table => new
                {
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.EmailAddress);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "enum",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalType",
                schema: "enum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SpeciesTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalType_SpeciesType_SpeciesTypeId",
                        column: x => x.SpeciesTypeId,
                        principalSchema: "enum",
                        principalTable: "SpeciesType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                schema: "application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10000, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeatworkName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Meatwork_MeatworkName",
                        column: x => x.MeatworkName,
                        principalSchema: "application",
                        principalTable: "Meatwork",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedule_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "enum",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Audit",
                schema: "account",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => new { x.UserId, x.Timestamp });
                    table.ForeignKey(
                        name: "FK_Audit_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "account",
                        principalTable: "User",
                        principalColumn: "EmailAddress",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                schema: "enum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AnimalTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grade_AnimalType_AnimalTypeId",
                        column: x => x.AnimalTypeId,
                        principalSchema: "enum",
                        principalTable: "AnimalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                schema: "application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PreviousDocumentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateProcessed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromDateProcessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProceedsToBankAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProceedsToCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Terms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GstRegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmCostCentre = table.Column<int>(type: "int", nullable: false),
                    PlantName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DairySupplierNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KillSheet = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    BookingRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldRepName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarrierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NaitNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsignedFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpeciesTypeId = table.Column<int>(type: "int", nullable: true),
                    ScheduleId = table.Column<int>(type: "int", nullable: true),
                    TransitId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StockTotal = table.Column<int>(type: "int", nullable: false),
                    StockWeightTotal = table.Column<double>(type: "float", nullable: false),
                    WeightCostTotal = table.Column<double>(type: "float", nullable: false),
                    DeductionCostTotal = table.Column<double>(type: "float", nullable: false),
                    PremiumCostTotal = table.Column<double>(type: "float", nullable: false),
                    NetCostTotal = table.Column<double>(type: "float", nullable: false),
                    GstCostTotal = table.Column<double>(type: "float", nullable: false),
                    GrossCostTotal = table.Column<double>(type: "float", nullable: false),
                    CalcStockTotal = table.Column<int>(type: "int", nullable: false),
                    CalcStockWeightTotal = table.Column<double>(type: "float", nullable: false),
                    CalcWeightCostTotal = table.Column<double>(type: "float", nullable: false),
                    CalcDeductionCostTotal = table.Column<double>(type: "float", nullable: false),
                    CalcPremiumCostTotal = table.Column<double>(type: "float", nullable: false),
                    CalcNetCostTotal = table.Column<double>(type: "float", nullable: false),
                    CalcGstCostTotal = table.Column<double>(type: "float", nullable: false),
                    CalcGrossCostTotal = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_Document_PreviousDocumentId",
                        column: x => x.PreviousDocumentId,
                        principalSchema: "application",
                        principalTable: "Document",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Document_Farm_FarmCostCentre",
                        column: x => x.FarmCostCentre,
                        principalSchema: "application",
                        principalTable: "Farm",
                        principalColumn: "CostCentre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Document_Plant_PlantName",
                        column: x => x.PlantName,
                        principalSchema: "application",
                        principalTable: "Plant",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Document_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "application",
                        principalTable: "Schedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Document_SpeciesType_SpeciesTypeId",
                        column: x => x.SpeciesTypeId,
                        principalSchema: "enum",
                        principalTable: "SpeciesType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Document_Status_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "enum",
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Document_TransitDto_TransitId",
                        column: x => x.TransitId,
                        principalSchema: "source",
                        principalTable: "TransitDto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Uplift",
                schema: "application",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uplift_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "application",
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Price",
                schema: "application",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    MinWeight = table.Column<double>(type: "float", nullable: false),
                    MaxWeight = table.Column<double>(type: "float", nullable: false),
                    Modifier = table.Column<double>(type: "float", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Price_Grade_GradeId",
                        column: x => x.GradeId,
                        principalSchema: "enum",
                        principalTable: "Grade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Price_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "application",
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animal",
                schema: "application",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KillAgenda = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DateKilled = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    NaitEid = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    NaitVisual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SffEarTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CondemnedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    SplitPaymentPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Retained = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetsOptimumRange = table.Column<bool>(type: "bit", nullable: false),
                    MeetsMasterGrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ph = table.Column<double>(type: "float", nullable: false),
                    Presentation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PresentationReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TailLengthDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishingContract = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InventoryCost = table.Column<int>(type: "int", nullable: false),
                    FinishingAmount = table.Column<int>(type: "int", nullable: false),
                    Defects = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockWeight = table.Column<double>(type: "float", nullable: false),
                    WeightCost = table.Column<double>(type: "float", nullable: false),
                    DeductionCost = table.Column<double>(type: "float", nullable: false),
                    PremiumCost = table.Column<double>(type: "float", nullable: false),
                    NetCost = table.Column<double>(type: "float", nullable: false),
                    GstCost = table.Column<double>(type: "float", nullable: false),
                    GrossCost = table.Column<double>(type: "float", nullable: false),
                    CalcStockWeight = table.Column<double>(type: "float", nullable: false),
                    CalcWeightCost = table.Column<double>(type: "float", nullable: false),
                    CalcDeductionCost = table.Column<double>(type: "float", nullable: false),
                    CalcPremiumCost = table.Column<double>(type: "float", nullable: false),
                    CalcNetCost = table.Column<double>(type: "float", nullable: false),
                    CalcGstCost = table.Column<double>(type: "float", nullable: false),
                    CalcGrossCost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animal_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "application",
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Animal_Grade_GradeId",
                        column: x => x.GradeId,
                        principalSchema: "enum",
                        principalTable: "Grade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalTypeSummary",
                schema: "application",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnimalTypeId = table.Column<int>(type: "int", nullable: false),
                    StockCount = table.Column<int>(type: "int", nullable: false),
                    StockWeightKg = table.Column<double>(type: "float", nullable: false),
                    StockCost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalTypeSummary", x => new { x.DocumentId, x.AnimalTypeId });
                    table.ForeignKey(
                        name: "FK_AnimalTypeSummary_AnimalType_AnimalTypeId",
                        column: x => x.AnimalTypeId,
                        principalSchema: "enum",
                        principalTable: "AnimalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalTypeSummary_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "application",
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeductionDetail",
                schema: "application",
                columns: table => new
                {
                    AnimalId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    PaymentSummaryAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductionDetail", x => new { x.AnimalId, x.Code });
                    table.ForeignKey(
                        name: "FK_DeductionDetail_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalSchema: "application",
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PremiumDetail",
                schema: "application",
                columns: table => new
                {
                    AnimalId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    PaymentSummaryAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiumDetail", x => new { x.AnimalId, x.Code });
                    table.ForeignKey(
                        name: "FK_PremiumDetail_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalSchema: "application",
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Read" },
                    { 1, "Read/Write" },
                    { 2, "Administrator" }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "SpeciesType",
                columns: new[] { "Id", "DisplayName", "Name" },
                values: new object[,]
                {
                    { 0, "Bobby", "BOBBY" },
                    { 1, "Cattle", "BOVINE" },
                    { 2, "Sheep", "OVINE" },
                    { 3, "Deer", "DEER" }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Pending" },
                    { 1, "Approved" },
                    { 2, "Declined" },
                    { 3, "Overridden" },
                    { 4, "Superseded" }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Validation",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Pending" },
                    { 1, "Low" },
                    { 2, "Valid" },
                    { 3, "High" }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "AnimalType",
                columns: new[] { "Id", "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[,]
                {
                    { 0, "Bobby", "BOBBY", 0 },
                    { 1, "Bull", "BULL", 1 },
                    { 2, "Cow", "COW", 1 },
                    { 3, "Manufacturing Cow", "MCOW", 1 },
                    { 4, "Heifer", "HEIFER", 1 },
                    { 5, "Steer", "STEER", 1 },
                    { 6, "Lamb", "LAMB", 2 },
                    { 7, "Mutton", "MUTTON", 2 },
                    { 8, "Ram", "RAM", 2 },
                    { 9, "Hind", "HIND", 3 },
                    { 10, "Stag", "STAG", 3 }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Grade",
                columns: new[] { "Id", "AnimalTypeId", "Name" },
                values: new object[,]
                {
                    { 0, 0, "BV" },
                    { 1, 0, "COND" },
                    { 2, 0, "DEAD" },
                    { 3, 1, "M1" },
                    { 4, 1, "M2" },
                    { 5, 1, "M3" },
                    { 6, 1, "TM1" },
                    { 7, 1, "TM2" },
                    { 8, 1, "TM3" },
                    { 9, 1, "COND" },
                    { 10, 1, "DEAD" },
                    { 11, 2, "F1" },
                    { 12, 2, "F2" },
                    { 13, 2, "F3" },
                    { 14, 2, "P1" },
                    { 15, 2, "P2" },
                    { 16, 2, "P3" },
                    { 17, 2, "T1" },
                    { 18, 2, "T2" },
                    { 19, 2, "T3" },
                    { 20, 2, "COND" },
                    { 21, 2, "DEAD" },
                    { 22, 3, "M" },
                    { 23, 3, "COND" },
                    { 24, 3, "DEAD" },
                    { 25, 4, "A1" },
                    { 26, 4, "A2" },
                    { 27, 4, "A3" },
                    { 28, 4, "F1" },
                    { 29, 4, "F2" },
                    { 30, 4, "F3" },
                    { 31, 4, "L1" },
                    { 32, 4, "L2" },
                    { 33, 4, "L3" },
                    { 34, 4, "M" },
                    { 35, 4, "P1" },
                    { 36, 4, "P2" },
                    { 37, 4, "P3" },
                    { 38, 4, "T1" },
                    { 39, 4, "T2" },
                    { 40, 4, "T3" },
                    { 41, 4, "COND" }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Grade",
                columns: new[] { "Id", "AnimalTypeId", "Name" },
                values: new object[,]
                {
                    { 42, 4, "DEAD" },
                    { 43, 5, "A1" },
                    { 44, 5, "A2" },
                    { 45, 5, "A3" },
                    { 46, 5, "F1" },
                    { 47, 5, "F2" },
                    { 48, 5, "F3" },
                    { 49, 5, "L1" },
                    { 50, 5, "L2" },
                    { 51, 5, "L3" },
                    { 52, 5, "M" },
                    { 53, 5, "P1" },
                    { 54, 5, "P2" },
                    { 55, 5, "P3" },
                    { 56, 5, "T1" },
                    { 57, 5, "T2" },
                    { 58, 5, "T3" },
                    { 59, 5, "COND" },
                    { 60, 5, "DEAD" },
                    { 61, 6, "A" },
                    { 62, 6, "B" },
                    { 63, 6, "C" },
                    { 64, 6, "F" },
                    { 65, 6, "M" },
                    { 66, 6, "P" },
                    { 67, 6, "T" },
                    { 68, 6, "Y" },
                    { 69, 6, "COND" },
                    { 70, 6, "DEAD" },
                    { 71, 7, "MF" },
                    { 72, 7, "MH" },
                    { 73, 7, "MM" },
                    { 74, 7, "MP" },
                    { 75, 7, "ML" },
                    { 76, 7, "MX" },
                    { 77, 7, "COND" },
                    { 78, 7, "DEAD" },
                    { 79, 8, "R" },
                    { 80, 8, "COND" },
                    { 81, 8, "DEAD" },
                    { 82, 9, "AF1" },
                    { 83, 9, "AF2" }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Grade",
                columns: new[] { "Id", "AnimalTypeId", "Name" },
                values: new object[,]
                {
                    { 84, 9, "AFH" },
                    { 85, 9, "AP" },
                    { 86, 9, "M1" },
                    { 87, 9, "M2" },
                    { 88, 9, "PD1" },
                    { 89, 9, "PD2" },
                    { 90, 9, "PLG" },
                    { 91, 9, "PLG1" },
                    { 92, 9, "PLG2" },
                    { 93, 9, "COND" },
                    { 94, 9, "DEAD" },
                    { 95, 10, "AF1" },
                    { 96, 10, "AF2" },
                    { 97, 10, "AFH" },
                    { 98, 10, "AP" },
                    { 99, 10, "M1" },
                    { 100, 10, "M2" },
                    { 101, 10, "PF1" },
                    { 102, 10, "PF2" },
                    { 103, 10, "PLG" },
                    { 104, 10, "PLG1" },
                    { 105, 10, "PLG2" },
                    { 106, 10, "COND" },
                    { 107, 10, "DEAD" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_DocumentId",
                schema: "application",
                table: "Animal",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_GradeId",
                schema: "application",
                table: "Animal",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalType_SpeciesTypeId",
                schema: "enum",
                table: "AnimalType",
                column: "SpeciesTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalTypeSummary_AnimalTypeId",
                schema: "application",
                table: "AnimalTypeSummary",
                column: "AnimalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_FarmCostCentre",
                schema: "application",
                table: "Document",
                column: "FarmCostCentre");

            migrationBuilder.CreateIndex(
                name: "IX_Document_PlantName",
                schema: "application",
                table: "Document",
                column: "PlantName");

            migrationBuilder.CreateIndex(
                name: "IX_Document_PreviousDocumentId",
                schema: "application",
                table: "Document",
                column: "PreviousDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_ScheduleId",
                schema: "application",
                table: "Document",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_SpeciesTypeId",
                schema: "application",
                table: "Document",
                column: "SpeciesTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_StatusId",
                schema: "application",
                table: "Document",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_TransitId",
                schema: "application",
                table: "Document",
                column: "TransitId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_AnimalTypeId",
                schema: "enum",
                table: "Grade",
                column: "AnimalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_MeatworkName",
                schema: "application",
                table: "Plant",
                column: "MeatworkName");

            migrationBuilder.CreateIndex(
                name: "IX_Price_GradeId",
                schema: "application",
                table: "Price",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "Price_Unique",
                schema: "application",
                table: "Price",
                columns: new[] { "ScheduleId", "GradeId", "MinWeight", "MaxWeight" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_MeatworkName",
                schema: "application",
                table: "Schedule",
                column: "MeatworkName");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_StatusId",
                schema: "application",
                table: "Schedule",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "Schedule_Unique",
                schema: "application",
                table: "Schedule",
                columns: new[] { "StartDate", "EndDate", "MeatworkName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_MeatworkName",
                schema: "application",
                table: "Supplier",
                column: "MeatworkName");

            migrationBuilder.CreateIndex(
                name: "Uplift_Unique",
                schema: "application",
                table: "Uplift",
                columns: new[] { "ScheduleId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                schema: "account",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalTypeSummary",
                schema: "application");

            migrationBuilder.DropTable(
                name: "Audit",
                schema: "account");

            migrationBuilder.DropTable(
                name: "DeductionDetail",
                schema: "application");

            migrationBuilder.DropTable(
                name: "DeductionDto",
                schema: "source");

            migrationBuilder.DropTable(
                name: "PremiumDetail",
                schema: "application");

            migrationBuilder.DropTable(
                name: "Price",
                schema: "application");

            migrationBuilder.DropTable(
                name: "Supplier",
                schema: "application");

            migrationBuilder.DropTable(
                name: "Uplift",
                schema: "application");

            migrationBuilder.DropTable(
                name: "Validation",
                schema: "enum");

            migrationBuilder.DropTable(
                name: "User",
                schema: "account");

            migrationBuilder.DropTable(
                name: "Animal",
                schema: "application");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "enum");

            migrationBuilder.DropTable(
                name: "Document",
                schema: "application");

            migrationBuilder.DropTable(
                name: "Grade",
                schema: "enum");

            migrationBuilder.DropTable(
                name: "Farm",
                schema: "application");

            migrationBuilder.DropTable(
                name: "Plant",
                schema: "application");

            migrationBuilder.DropTable(
                name: "Schedule",
                schema: "application");

            migrationBuilder.DropTable(
                name: "TransitDto",
                schema: "source");

            migrationBuilder.DropTable(
                name: "AnimalType",
                schema: "enum");

            migrationBuilder.DropTable(
                name: "Meatwork",
                schema: "application");

            migrationBuilder.DropTable(
                name: "Status",
                schema: "enum");

            migrationBuilder.DropTable(
                name: "SpeciesType",
                schema: "enum");
        }
    }
}
