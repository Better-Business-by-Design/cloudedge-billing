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
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
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
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
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
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
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
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
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
                    RoleId = table.Column<byte>(type: "tinyint", nullable: false)
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
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    SpeciesTypeId = table.Column<byte>(type: "tinyint", nullable: false),
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
                    StatusId = table.Column<byte>(type: "tinyint", nullable: false)
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
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    AnimalTypeId = table.Column<byte>(type: "tinyint", nullable: false),
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
                    KillSheet = table.Column<long>(type: "bigint", nullable: false),
                    BookingRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldRepName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarrierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NaitNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsignedFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpeciesTypeId = table.Column<byte>(type: "tinyint", nullable: true),
                    ScheduleId = table.Column<int>(type: "int", nullable: true),
                    TransitId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    StockCount = table.Column<int>(type: "int", nullable: false),
                    WeightTotal = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    WeightCostTotal = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    DeductionCostTotal = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    PremiumCostTotal = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    NetCostTotal = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    CalcWeightCostTotal = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    CalcDeductionCostTotal = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    CalcPremiumCostTotal = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    CalcNetCostTotal = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    CalcTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    AnimalTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    MinWeight = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    MaxWeight = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uplift_AnimalType_AnimalTypeId",
                        column: x => x.AnimalTypeId,
                        principalSchema: "enum",
                        principalTable: "AnimalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    GradeId = table.Column<byte>(type: "tinyint", nullable: false),
                    MinWeight = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    MaxWeight = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    Modifier = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false)
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
                    KillAgenda = table.Column<long>(type: "bigint", nullable: false),
                    DateKilled = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GradeId = table.Column<byte>(type: "tinyint", nullable: false),
                    NaitEid = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    NaitVisual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SffEarTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CondemnedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SplitPaymentPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Retained = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetsOptimumRange = table.Column<bool>(type: "bit", nullable: false),
                    MeetsMasterGrade = table.Column<bool>(type: "bit", nullable: false),
                    Ph = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    Presentation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PresentationReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TailLengthDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishingContract = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InventoryCost = table.Column<int>(type: "int", nullable: false),
                    FinishingAmount = table.Column<int>(type: "int", nullable: false),
                    Defects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    WeightCost = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    DeductionCost = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    PremiumCost = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    NetCost = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    CalcPrice = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    CalcWeightCost = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    CalcDeductionCost = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    CalcPremiumCost = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    CalcNetCost = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false)
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
                    AnimalTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    StockCount = table.Column<int>(type: "int", nullable: false),
                    StockWeightKg = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    StockCost = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false)
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
                    Rate = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    PaymentSummaryAmount = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false)
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
                    Rate = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    PaymentSummaryAmount = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false)
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
                    { (byte)0, "Read" },
                    { (byte)1, "Read/Write" },
                    { (byte)2, "Administrator" },
                    { (byte)3, "None" }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "SpeciesType",
                columns: new[] { "Id", "DisplayName", "Name" },
                values: new object[,]
                {
                    { (byte)0, "Bobby", "BOBBY" },
                    { (byte)1, "Cattle", "BOVINE" },
                    { (byte)2, "Sheep", "OVINE" },
                    { (byte)3, "Deer", "DEER" }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)0, "Pending" },
                    { (byte)1, "Approved" },
                    { (byte)2, "Declined" },
                    { (byte)3, "Overridden" },
                    { (byte)4, "Superseded" },
                    { (byte)5, "None" }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Validation",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)0, "Pending" },
                    { (byte)1, "Low" },
                    { (byte)2, "Valid" },
                    { (byte)3, "High" }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "AnimalType",
                columns: new[] { "Id", "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[,]
                {
                    { (byte)0, "Bobby Calves", "BOBBY", (byte)0 },
                    { (byte)1, "Bull", "BULL", (byte)1 },
                    { (byte)2, "Cow", "COW", (byte)1 },
                    { (byte)3, "Manufacturing Cow", "MCOW", (byte)1 },
                    { (byte)4, "Heifer", "HEIFER", (byte)1 },
                    { (byte)5, "Steer", "STEER", (byte)1 },
                    { (byte)6, "Lamb", "LAMB", (byte)2 },
                    { (byte)7, "Mutton", "MUTTON", (byte)2 },
                    { (byte)8, "Ram", "RAM", (byte)2 },
                    { (byte)9, "Hind", "HIND", (byte)3 },
                    { (byte)10, "Stag", "STAG", (byte)3 }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Grade",
                columns: new[] { "Id", "AnimalTypeId", "Name" },
                values: new object[,]
                {
                    { (byte)0, (byte)0, "BV" },
                    { (byte)1, (byte)0, "COND" },
                    { (byte)2, (byte)0, "DEAD" },
                    { (byte)3, (byte)1, "M1" },
                    { (byte)4, (byte)1, "M2" },
                    { (byte)5, (byte)1, "M3" },
                    { (byte)6, (byte)1, "TM1" },
                    { (byte)7, (byte)1, "TM2" },
                    { (byte)8, (byte)1, "TM3" },
                    { (byte)9, (byte)1, "COND" },
                    { (byte)10, (byte)1, "DEAD" },
                    { (byte)11, (byte)2, "F1" },
                    { (byte)12, (byte)2, "F2" },
                    { (byte)13, (byte)2, "F3" },
                    { (byte)14, (byte)2, "P1" },
                    { (byte)15, (byte)2, "P2" },
                    { (byte)16, (byte)2, "P3" },
                    { (byte)17, (byte)2, "T1" },
                    { (byte)18, (byte)2, "T2" },
                    { (byte)19, (byte)2, "T3" },
                    { (byte)20, (byte)2, "COND" },
                    { (byte)21, (byte)2, "DEAD" },
                    { (byte)22, (byte)3, "M" },
                    { (byte)23, (byte)3, "COND" },
                    { (byte)24, (byte)3, "DEAD" },
                    { (byte)25, (byte)4, "A1" },
                    { (byte)26, (byte)4, "A2" },
                    { (byte)27, (byte)4, "A3" },
                    { (byte)28, (byte)4, "F1" },
                    { (byte)29, (byte)4, "F2" },
                    { (byte)30, (byte)4, "F3" },
                    { (byte)31, (byte)4, "L1" },
                    { (byte)32, (byte)4, "L2" },
                    { (byte)33, (byte)4, "L3" },
                    { (byte)34, (byte)4, "M" },
                    { (byte)35, (byte)4, "P1" },
                    { (byte)36, (byte)4, "P2" },
                    { (byte)37, (byte)4, "P3" },
                    { (byte)38, (byte)4, "T1" },
                    { (byte)39, (byte)4, "T2" },
                    { (byte)40, (byte)4, "T3" },
                    { (byte)41, (byte)4, "COND" }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Grade",
                columns: new[] { "Id", "AnimalTypeId", "Name" },
                values: new object[,]
                {
                    { (byte)42, (byte)4, "DEAD" },
                    { (byte)43, (byte)5, "A1" },
                    { (byte)44, (byte)5, "A2" },
                    { (byte)45, (byte)5, "A3" },
                    { (byte)46, (byte)5, "F1" },
                    { (byte)47, (byte)5, "F2" },
                    { (byte)48, (byte)5, "F3" },
                    { (byte)49, (byte)5, "L1" },
                    { (byte)50, (byte)5, "L2" },
                    { (byte)51, (byte)5, "L3" },
                    { (byte)52, (byte)5, "M" },
                    { (byte)53, (byte)5, "P1" },
                    { (byte)54, (byte)5, "P2" },
                    { (byte)55, (byte)5, "P3" },
                    { (byte)56, (byte)5, "T1" },
                    { (byte)57, (byte)5, "T2" },
                    { (byte)58, (byte)5, "T3" },
                    { (byte)59, (byte)5, "COND" },
                    { (byte)60, (byte)5, "DEAD" },
                    { (byte)61, (byte)6, "A" },
                    { (byte)62, (byte)6, "B" },
                    { (byte)63, (byte)6, "C" },
                    { (byte)64, (byte)6, "F" },
                    { (byte)65, (byte)6, "M" },
                    { (byte)66, (byte)6, "P" },
                    { (byte)67, (byte)6, "T" },
                    { (byte)68, (byte)6, "Y" },
                    { (byte)69, (byte)6, "COND" },
                    { (byte)70, (byte)6, "DEAD" },
                    { (byte)71, (byte)7, "MF" },
                    { (byte)72, (byte)7, "MH" },
                    { (byte)73, (byte)7, "MM" },
                    { (byte)74, (byte)7, "MP" },
                    { (byte)75, (byte)7, "ML" },
                    { (byte)76, (byte)7, "MX" },
                    { (byte)77, (byte)7, "COND" },
                    { (byte)78, (byte)7, "DEAD" },
                    { (byte)79, (byte)8, "R" },
                    { (byte)80, (byte)8, "COND" },
                    { (byte)81, (byte)8, "DEAD" },
                    { (byte)82, (byte)9, "AF1" },
                    { (byte)83, (byte)9, "AF2" }
                });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Grade",
                columns: new[] { "Id", "AnimalTypeId", "Name" },
                values: new object[,]
                {
                    { (byte)84, (byte)9, "AFH" },
                    { (byte)85, (byte)9, "AP" },
                    { (byte)86, (byte)9, "M1" },
                    { (byte)87, (byte)9, "M2" },
                    { (byte)88, (byte)9, "PD1" },
                    { (byte)89, (byte)9, "PD2" },
                    { (byte)90, (byte)9, "PLG" },
                    { (byte)91, (byte)9, "PLG1" },
                    { (byte)92, (byte)9, "PLG2" },
                    { (byte)93, (byte)9, "COND" },
                    { (byte)94, (byte)9, "DEAD" },
                    { (byte)95, (byte)10, "AF1" },
                    { (byte)96, (byte)10, "AF2" },
                    { (byte)97, (byte)10, "AFH" },
                    { (byte)98, (byte)10, "AP" },
                    { (byte)99, (byte)10, "M1" },
                    { (byte)100, (byte)10, "M2" },
                    { (byte)101, (byte)10, "PF1" },
                    { (byte)102, (byte)10, "PF2" },
                    { (byte)103, (byte)10, "PLG" },
                    { (byte)104, (byte)10, "PLG1" },
                    { (byte)105, (byte)10, "PLG2" },
                    { (byte)106, (byte)10, "COND" },
                    { (byte)107, (byte)10, "DEAD" }
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
                name: "IX_Uplift_AnimalTypeId",
                schema: "application",
                table: "Uplift",
                column: "AnimalTypeId");

            migrationBuilder.CreateIndex(
                name: "Uplift_Unique",
                schema: "application",
                table: "Uplift",
                columns: new[] { "ScheduleId", "Name", "AnimalTypeId" },
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
