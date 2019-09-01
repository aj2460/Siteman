using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteManagement.Migrations
{
    public partial class EmployeeCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "labours");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeCategory",
                table: "labours",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeCategoryId",
                table: "labours",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "labours");

            migrationBuilder.DropColumn(
                name: "EmployeeCategoryId",
                table: "labours");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "labours",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
