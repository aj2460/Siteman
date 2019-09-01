using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteManagement.Migrations
{
    public partial class AddEmployeeCategoryinDatabseSec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                table: "labours");

            migrationBuilder.DropColumn(
                name: "LabourExpenseId",
                table: "labours");

            migrationBuilder.DropColumn(
                name: "LabourReceiptId",
                table: "labours");

            migrationBuilder.DropColumn(
                name: "MaterialExpenseId",
                table: "labours");

            migrationBuilder.CreateIndex(
                name: "IX_labours_EmployeeCategoryId",
                table: "labours",
                column: "EmployeeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_labours_EmployeeCategories_EmployeeCategoryId",
                table: "labours",
                column: "EmployeeCategoryId",
                principalTable: "EmployeeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_labours_EmployeeCategories_EmployeeCategoryId",
                table: "labours");

            migrationBuilder.DropIndex(
                name: "IX_labours_EmployeeCategoryId",
                table: "labours");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeCategory",
                table: "labours",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LabourExpenseId",
                table: "labours",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LabourReceiptId",
                table: "labours",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaterialExpenseId",
                table: "labours",
                nullable: false,
                defaultValue: 0);
        }
    }
}
