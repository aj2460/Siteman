using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteManagement.Migrations
{
    public partial class addInvoiceAndSupplierToMaterialEx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Particular",
                table: "MaterialExpenses");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "MaterialExpenses",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "MaterialExpenses",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Particular",
                table: "LabourExpenses",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "MaterialExpenses");

            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "MaterialExpenses");

            migrationBuilder.AddColumn<string>(
                name: "Particular",
                table: "MaterialExpenses",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Particular",
                table: "LabourExpenses",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}
