using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteManagement.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabourExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SiteId = table.Column<int>(nullable: false),
                    LabourId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Wage = table.Column<float>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabourExpenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "labourReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LabourId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<float>(nullable: false),
                    Dscription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_labourReceipts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "labours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Wage = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_labours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SiteId = table.Column<int>(nullable: false),
                    LabourId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Particular = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialExpenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabourExpenses");

            migrationBuilder.DropTable(
                name: "labourReceipts");

            migrationBuilder.DropTable(
                name: "labours");

            migrationBuilder.DropTable(
                name: "MaterialExpenses");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
