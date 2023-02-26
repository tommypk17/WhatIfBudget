using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatIfBudget.Data.Migrations
{
    public partial class updateInvestmentTableColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualRaiseFactor_Percent",
                table: "InvestmentGoals");

            migrationBuilder.RenameColumn(
                name: "YearsToMaturation",
                table: "InvestmentGoals",
                newName: "YearsToTarget");

            migrationBuilder.AddColumn<double>(
                name: "AdditionalBudgetAllocation",
                table: "SavingGoals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AdditionalBudgetAllocation",
                table: "MortgageGoals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AdditionalBudgetAllocation",
                table: "DebtGoals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "InvestmentGoalInvestments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvestmentGoalId = table.Column<int>(type: "int", nullable: false),
                    InvestmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentGoalInvestments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentGoalInvestments_InvestmentGoals_InvestmentGoalId",
                        column: x => x.InvestmentGoalId,
                        principalTable: "InvestmentGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentGoalInvestments_Investments_InvestmentId",
                        column: x => x.InvestmentId,
                        principalTable: "Investments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentGoalInvestments_InvestmentGoalId",
                table: "InvestmentGoalInvestments",
                column: "InvestmentGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentGoalInvestments_InvestmentId",
                table: "InvestmentGoalInvestments",
                column: "InvestmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentGoalInvestments");

            migrationBuilder.DropColumn(
                name: "AdditionalBudgetAllocation",
                table: "SavingGoals");

            migrationBuilder.DropColumn(
                name: "AdditionalBudgetAllocation",
                table: "MortgageGoals");

            migrationBuilder.DropColumn(
                name: "AdditionalBudgetAllocation",
                table: "DebtGoals");

            migrationBuilder.RenameColumn(
                name: "YearsToTarget",
                table: "InvestmentGoals",
                newName: "YearsToMaturation");

            migrationBuilder.AddColumn<double>(
                name: "AnnualRaiseFactor_Percent",
                table: "InvestmentGoals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
