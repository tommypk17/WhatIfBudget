using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatIfBudget.Data.Migrations
{
    public partial class addNameToIncomeAndExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Incomes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Expenses");
        }
    }
}
