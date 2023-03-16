using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatIfBudget.Data.Migrations
{
    public partial class updateSavingGoalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalBalance",
                table: "SavingGoals",
                newName: "TargetBalance");

            migrationBuilder.AddColumn<double>(
                name: "CurrentBalance",
                table: "SavingGoals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentBalance",
                table: "SavingGoals");

            migrationBuilder.RenameColumn(
                name: "TargetBalance",
                table: "SavingGoals",
                newName: "TotalBalance");
        }
    }
}
