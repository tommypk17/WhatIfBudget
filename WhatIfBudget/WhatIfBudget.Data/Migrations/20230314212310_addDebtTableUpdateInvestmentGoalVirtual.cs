using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatIfBudget.Data.Migrations
{
    public partial class addDebtTableUpdateInvestmentGoalVirtual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Budgets_InvestmentGoalId",
                table: "Budgets");

            migrationBuilder.CreateTable(
                name: "Debts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentBalance = table.Column<double>(type: "float", nullable: false),
                    InterestRate = table.Column<float>(type: "real", nullable: false),
                    MinimumPayment = table.Column<double>(type: "float", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DebtGoalDebts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DebtGoalId = table.Column<int>(type: "int", nullable: false),
                    DebtId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtGoalDebts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DebtGoalDebts_DebtGoals_DebtGoalId",
                        column: x => x.DebtGoalId,
                        principalTable: "DebtGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DebtGoalDebts_Debts_DebtId",
                        column: x => x.DebtId,
                        principalTable: "Debts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_InvestmentGoalId",
                table: "Budgets",
                column: "InvestmentGoalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DebtGoalDebts_DebtGoalId",
                table: "DebtGoalDebts",
                column: "DebtGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_DebtGoalDebts_DebtId",
                table: "DebtGoalDebts",
                column: "DebtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DebtGoalDebts");

            migrationBuilder.DropTable(
                name: "Debts");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_InvestmentGoalId",
                table: "Budgets");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_InvestmentGoalId",
                table: "Budgets",
                column: "InvestmentGoalId");
        }
    }
}
