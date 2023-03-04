using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatIfBudget.Data.Migrations
{
    public partial class removeTotalBalanceFromInvestmentGoalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalBalance",
                table: "InvestmentGoals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalBalance",
                table: "InvestmentGoals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
