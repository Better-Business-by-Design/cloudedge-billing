using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudEdgeBilling.BAL.Migrations
{
    public partial class DocVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "DocumentVersion",
                schema: "application",
                table: "Document",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentVersion",
                schema: "application",
                table: "Document");
        }
    }
}
