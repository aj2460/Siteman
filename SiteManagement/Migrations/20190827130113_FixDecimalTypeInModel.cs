using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteManagement.Migrations
{
    public partial class FixDecimalTypeInModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "MaterialExpenses",
                type: "money",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "labourReceipts",
                type: "money",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "Wage",
                table: "LabourExpenses",
                type: "money",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Amount",
                table: "MaterialExpenses",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<float>(
                name: "Amount",
                table: "labourReceipts",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<float>(
                name: "Wage",
                table: "LabourExpenses",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");
        }
    }
}
