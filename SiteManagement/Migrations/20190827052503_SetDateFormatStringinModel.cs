using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteManagement.Migrations
{
    public partial class SetDateFormatStringinModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Day",
                table: "LabourExpenses",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_MaterialExpenses_LabourId",
                table: "MaterialExpenses",
                column: "LabourId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialExpenses_labours_LabourId",
                table: "MaterialExpenses",
                column: "LabourId",
                principalTable: "labours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialExpenses_labours_LabourId",
                table: "MaterialExpenses");

            migrationBuilder.DropIndex(
                name: "IX_MaterialExpenses_LabourId",
                table: "MaterialExpenses");

            migrationBuilder.AlterColumn<int>(
                name: "Day",
                table: "LabourExpenses",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
