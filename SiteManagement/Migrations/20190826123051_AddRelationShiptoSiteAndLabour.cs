using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteManagement.Migrations
{
    public partial class AddRelationShiptoSiteAndLabour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MaterialExpenses_SiteId",
                table: "MaterialExpenses",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_LabourExpenses_LabourId",
                table: "LabourExpenses",
                column: "LabourId");

            migrationBuilder.CreateIndex(
                name: "IX_LabourExpenses_SiteId",
                table: "LabourExpenses",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabourExpenses_labours_LabourId",
                table: "LabourExpenses",
                column: "LabourId",
                principalTable: "labours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LabourExpenses_Sites_SiteId",
                table: "LabourExpenses",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialExpenses_Sites_SiteId",
                table: "MaterialExpenses",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabourExpenses_labours_LabourId",
                table: "LabourExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_LabourExpenses_Sites_SiteId",
                table: "LabourExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialExpenses_Sites_SiteId",
                table: "MaterialExpenses");

            migrationBuilder.DropIndex(
                name: "IX_MaterialExpenses_SiteId",
                table: "MaterialExpenses");

            migrationBuilder.DropIndex(
                name: "IX_LabourExpenses_LabourId",
                table: "LabourExpenses");

            migrationBuilder.DropIndex(
                name: "IX_LabourExpenses_SiteId",
                table: "LabourExpenses");
        }
    }
}
