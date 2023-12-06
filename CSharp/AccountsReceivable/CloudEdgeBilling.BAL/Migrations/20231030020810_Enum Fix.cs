using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudEdgeBilling.BAL.Migrations
{
    public partial class EnumFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)0,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "None", "MISSING" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)1,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Bobby Calves", "BOBBY" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)2,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Bull", "BULL", (byte)2 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)3,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Cow", "COW", (byte)2 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)4,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Manufacturing Cow", "MCOW", (byte)2 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)5,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Heifer", "HEIFER", (byte)2 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)6,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Steer", "STEER" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)7,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Lamb", "LAMB", (byte)3 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)8,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Mutton", "MUTTON", (byte)3 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)9,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Ram", "RAM" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "None");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)1,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)1, "BV" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)2,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)1, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)3,
                column: "Name",
                value: "DEAD");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)4,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "M1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)5,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "M2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)6,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "M3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)7,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "TM1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)8,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "TM2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)9,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "TM3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)10,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)11,
                column: "Name",
                value: "DEAD");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)12,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)3, "F1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)13,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)3, "F2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)14,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)3, "F3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)15,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)3, "P1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)16,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)3, "P2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)17,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)3, "P3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)18,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)3, "T1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)19,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)3, "T2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)20,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)3, "T3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)21,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)3, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)22,
                column: "Name",
                value: "DEAD");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)23,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "M" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)24,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)25,
                column: "Name",
                value: "DEAD");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)26,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "A1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)27,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "A2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)28,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "A3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)29,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "F1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)30,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "F2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)31,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "F3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)32,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "L1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)33,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "L2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)34,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "L3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)35,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "M" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)36,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "P1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)37,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "P2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)38,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "P3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)39,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "T1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)40,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "T2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)41,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "T3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)42,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)43,
                column: "Name",
                value: "DEAD");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)44,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "A1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)45,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "A2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)46,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "A3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)47,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "F1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)48,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "F2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)49,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "F3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)50,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "L1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)51,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "L2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)52,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "L3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)53,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "M" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)54,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "P1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)55,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "P2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)56,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "P3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)57,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "T1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)58,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "T2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)59,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "T3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)60,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)61,
                column: "Name",
                value: "DEAD");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)62,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "A" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)63,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "B" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)64,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "C" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)65,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "F" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)66,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "M" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)67,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "P" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)68,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "T" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)69,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "Y" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)70,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)71,
                column: "Name",
                value: "DEAD");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)72,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)8, "MF" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)73,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)8, "MH" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)74,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)8, "MM" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)75,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)8, "MP" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)76,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)8, "ML" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)77,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)8, "MX" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)78,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)8, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)79,
                column: "Name",
                value: "DEAD");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)80,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "R" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)81,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)82,
                column: "Name",
                value: "DEAD");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)83,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "AF1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)84,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "AF2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)85,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "AFH" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)86,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "AP" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)87,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "M1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)88,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "M2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)89,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "PD1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)90,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "PD2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)91,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "PLG" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)92,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "PLG1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)93,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "PLG2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)94,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)95,
                column: "Name",
                value: "DEAD");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Role",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "None");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Role",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "Name",
                value: "Read");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Role",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "Name",
                value: "Read/Write");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Role",
                keyColumn: "Id",
                keyValue: (byte)3,
                column: "Name",
                value: "Administrator");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "SpeciesType",
                keyColumn: "Id",
                keyValue: (byte)0,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "None", "MISSING" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "SpeciesType",
                keyColumn: "Id",
                keyValue: (byte)1,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Bobby", "BOBBY" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "SpeciesType",
                keyColumn: "Id",
                keyValue: (byte)2,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Cattle", "BOVINE" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "SpeciesType",
                keyColumn: "Id",
                keyValue: (byte)3,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Sheep", "OVINE" });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "SpeciesType",
                columns: new[] { "Id", "DisplayName", "Name" },
                values: new object[] { (byte)4, "Deer", "DEER" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "Missing");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "Name",
                value: "Pending");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "Name",
                value: "Approved");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)3,
                column: "Name",
                value: "Declined");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)4,
                column: "Name",
                value: "Overridden");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)5,
                column: "Name",
                value: "Superseded");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Validation",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "Missing");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Validation",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "Name",
                value: "Pending");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Validation",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "Name",
                value: "Low");

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Validation",
                columns: new[] { "Id", "Name" },
                values: new object[] { (byte)4, "Valid" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)10,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Hind", "HIND", (byte)4 });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "AnimalType",
                columns: new[] { "Id", "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { (byte)11, "Stag", "STAG", (byte)4 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)96,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)11, "AF1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)97,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)11, "AF2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)98,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)11, "AFH" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)99,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)11, "AP" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)100,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)11, "M1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)101,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)11, "M2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)102,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)11, "PF1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)103,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)11, "PF2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)104,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)11, "PLG" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)105,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)11, "PLG1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)106,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)11, "PLG2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)107,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)11, "COND" });

            migrationBuilder.InsertData(
                schema: "enum",
                table: "Grade",
                columns: new[] { "Id", "AnimalTypeId", "Name" },
                values: new object[] { (byte)108, (byte)11, "DEAD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)108);

            migrationBuilder.DeleteData(
                schema: "enum",
                table: "Validation",
                keyColumn: "Id",
                keyValue: (byte)4);

            migrationBuilder.DeleteData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)11);

            migrationBuilder.DeleteData(
                schema: "enum",
                table: "SpeciesType",
                keyColumn: "Id",
                keyValue: (byte)4);

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)0,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Bobby Calves", "BOBBY" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)1,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Bull", "BULL" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)2,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Cow", "COW", (byte)1 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)3,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Manufacturing Cow", "MCOW", (byte)1 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)4,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Heifer", "HEIFER", (byte)1 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)5,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Steer", "STEER", (byte)1 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)6,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Lamb", "LAMB" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)7,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Mutton", "MUTTON", (byte)2 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)8,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Ram", "RAM", (byte)2 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)9,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Hind", "HIND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "AnimalType",
                keyColumn: "Id",
                keyValue: (byte)10,
                columns: new[] { "DisplayName", "Name", "SpeciesTypeId" },
                values: new object[] { "Stag", "STAG", (byte)3 });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "BV");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)1,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)0, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)2,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)0, "DEAD" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)3,
                column: "Name",
                value: "M1");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)4,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)1, "M2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)5,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)1, "M3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)6,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)1, "TM1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)7,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)1, "TM2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)8,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)1, "TM3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)9,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)1, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)10,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)1, "DEAD" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)11,
                column: "Name",
                value: "F1");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)12,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "F2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)13,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "F3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)14,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "P1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)15,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "P2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)16,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "P3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)17,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "T1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)18,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "T2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)19,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "T3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)20,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)21,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)2, "DEAD" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)22,
                column: "Name",
                value: "M");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)23,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)3, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)24,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)3, "DEAD" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)25,
                column: "Name",
                value: "A1");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)26,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "A2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)27,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "A3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)28,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "F1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)29,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "F2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)30,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "F3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)31,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "L1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)32,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "L2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)33,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "L3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)34,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "M" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)35,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "P1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)36,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "P2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)37,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "P3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)38,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "T1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)39,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "T2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)40,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "T3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)41,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)42,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)4, "DEAD" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)43,
                column: "Name",
                value: "A1");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)44,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "A2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)45,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "A3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)46,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "F1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)47,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "F2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)48,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "F3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)49,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "L1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)50,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "L2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)51,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "L3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)52,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "M" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)53,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "P1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)54,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "P2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)55,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "P3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)56,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "T1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)57,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "T2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)58,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "T3" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)59,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)60,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)5, "DEAD" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)61,
                column: "Name",
                value: "A");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)62,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "B" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)63,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "C" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)64,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "F" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)65,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "M" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)66,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "P" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)67,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "T" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)68,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "Y" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)69,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)70,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)6, "DEAD" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)71,
                column: "Name",
                value: "MF");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)72,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "MH" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)73,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "MM" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)74,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "MP" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)75,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "ML" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)76,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "MX" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)77,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)78,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)7, "DEAD" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)79,
                column: "Name",
                value: "R");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)80,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)8, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)81,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)8, "DEAD" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)82,
                column: "Name",
                value: "AF1");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)83,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "AF2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)84,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "AFH" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)85,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "AP" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)86,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "M1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)87,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "M2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)88,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "PD1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)89,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "PD2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)90,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "PLG" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)91,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "PLG1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)92,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "PLG2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)93,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)94,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)9, "DEAD" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)95,
                column: "Name",
                value: "AF1");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)96,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "AF2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)97,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "AFH" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)98,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "AP" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)99,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "M1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)100,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "M2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)101,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "PF1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)102,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "PF2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)103,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "PLG" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)104,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "PLG1" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)105,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "PLG2" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)106,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "COND" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Grade",
                keyColumn: "Id",
                keyValue: (byte)107,
                columns: new[] { "AnimalTypeId", "Name" },
                values: new object[] { (byte)10, "DEAD" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Role",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "Read");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Role",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "Name",
                value: "Read/Write");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Role",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "Name",
                value: "Administrator");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Role",
                keyColumn: "Id",
                keyValue: (byte)3,
                column: "Name",
                value: "Missing");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "SpeciesType",
                keyColumn: "Id",
                keyValue: (byte)0,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Bobby", "BOBBY" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "SpeciesType",
                keyColumn: "Id",
                keyValue: (byte)1,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Cattle", "BOVINE" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "SpeciesType",
                keyColumn: "Id",
                keyValue: (byte)2,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Sheep", "OVINE" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "SpeciesType",
                keyColumn: "Id",
                keyValue: (byte)3,
                columns: new[] { "DisplayName", "Name" },
                values: new object[] { "Deer", "DEER" });

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "Pending");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "Name",
                value: "Approved");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "Name",
                value: "Declined");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)3,
                column: "Name",
                value: "Overridden");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)4,
                column: "Name",
                value: "Superseded");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Status",
                keyColumn: "Id",
                keyValue: (byte)5,
                column: "Name",
                value: "Missing");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Validation",
                keyColumn: "Id",
                keyValue: (byte)0,
                column: "Name",
                value: "Pending");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Validation",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "Name",
                value: "Low");

            migrationBuilder.UpdateData(
                schema: "enum",
                table: "Validation",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "Name",
                value: "Valid");
        }
    }
}
