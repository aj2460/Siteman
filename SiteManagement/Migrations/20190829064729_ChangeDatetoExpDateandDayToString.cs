using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteManagement.Migrations
{
    public partial class ChangeDatetoExpDateandDayToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "LabourExpenses",
                newName: "ExpDate");

            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "LabourExpenses",
                nullable: true,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpDate",
                table: "LabourExpenses",
                newName: "Date");

            migrationBuilder.AlterColumn<float>(
                name: "Day",
                table: "LabourExpenses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
