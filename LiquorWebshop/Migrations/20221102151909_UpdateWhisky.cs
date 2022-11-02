using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquorWebshop.Migrations
{
    public partial class UpdateWhisky : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WhiskyType",
                table: "Whiskies",
                newName: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Whiskies",
                newName: "WhiskyType");
        }
    }
}
