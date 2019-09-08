using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteManagement.Migrations
{
    public partial class InitialMigrationSecSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 250, nullable: false),
                    Place = table.Column<string>(maxLength: 250, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "labours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeCategoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Wage = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_labours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_labours_EmployeeCategories_EmployeeCategoryId",
                        column: x => x.EmployeeCategoryId,
                        principalTable: "EmployeeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabourExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SiteId = table.Column<int>(nullable: false),
                    LabourId = table.Column<int>(nullable: false),
                    ExpenseTypeId = table.Column<int>(nullable: false),
                    ExpDate = table.Column<DateTime>(nullable: false),
                    Particular = table.Column<string>(maxLength: 250, nullable: false),
                    Day = table.Column<string>(nullable: true),
                    Wage = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabourExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabourExpenses_ExpType_ExpenseTypeId",
                        column: x => x.ExpenseTypeId,
                        principalTable: "ExpType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabourExpenses_labours_LabourId",
                        column: x => x.LabourId,
                        principalTable: "labours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabourExpenses_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "labourReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LabourId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Dscription = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_labourReceipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_labourReceipts_labours_LabourId",
                        column: x => x.LabourId,
                        principalTable: "labours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Particular = table.Column<string>(maxLength: 250, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialExpenses_labours_LabourId",
                        column: x => x.LabourId,
                        principalTable: "labours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialExpenses_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabourExpenses_ExpenseTypeId",
                table: "LabourExpenses",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LabourExpenses_LabourId",
                table: "LabourExpenses",
                column: "LabourId");

            migrationBuilder.CreateIndex(
                name: "IX_LabourExpenses_SiteId",
                table: "LabourExpenses",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_labourReceipts_LabourId",
                table: "labourReceipts",
                column: "LabourId");

            migrationBuilder.CreateIndex(
                name: "IX_labours_EmployeeCategoryId",
                table: "labours",
                column: "EmployeeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialExpenses_LabourId",
                table: "MaterialExpenses",
                column: "LabourId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialExpenses_SiteId",
                table: "MaterialExpenses",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabourExpenses");

            migrationBuilder.DropTable(
                name: "labourReceipts");

            migrationBuilder.DropTable(
                name: "MaterialExpenses");

            migrationBuilder.DropTable(
                name: "ExpType");

            migrationBuilder.DropTable(
                name: "labours");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "EmployeeCategories");
        }
    }
}
