using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatIfBudget.Data.Migrations
{
    public partial class removeRaiseFactorAndAddGoalsBudgetAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualRaiseFactor_Percent",
                table: "InvestmentGoals");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalBudgetAllocation",
                table: "SavingGoals");

            migrationBuilder.DropColumn(
                name: "AdditionalBudgetAllocation",
                table: "MortgageGoals");

            migrationBuilder.DropColumn(
                name: "AdditionalBudgetAllocation",
                table: "DebtGoals");

            migrationBuilder.AddColumn<double>(
                name: "AnnualRaiseFactor_Percent",
                table: "InvestmentGoals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
