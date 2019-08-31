using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteManagement.Migrations
{
    public partial class LabourReeationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_labourReceipts_LabourId",
                table: "labourReceipts",
                column: "LabourId");

            migrationBuilder.AddForeignKey(
                name: "FK_labourReceipts_labours_LabourId",
                table: "labourReceipts",
                column: "LabourId",
                principalTable: "labours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_labourReceipts_labours_LabourId",
                table: "labourReceipts");

            migrationBuilder.DropIndex(
                name: "IX_labourReceipts_LabourId",
                table: "labourReceipts");

            migrationBuilder.DropColumn(
                name: "LabourExpenseId",
                table: "labours");

            migrationBuilder.DropColumn(
                name: "LabourReceiptId",
                table: "labours");

            migrationBuilder.DropColumn(
                name: "MaterialExpenseId",
                table: "labours");
        }
    }
}
