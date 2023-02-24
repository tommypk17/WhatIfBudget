using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatIfBudget.Data.Migrations
{
    public partial class pluralizeGoalTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_DebtGoal_DebtGoalId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_MortgageGoal_MortgageGoalId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_SavingGoal_SavingGoalId",
                table: "Budgets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavingGoal",
                table: "SavingGoal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MortgageGoal",
                table: "MortgageGoal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DebtGoal",
                table: "DebtGoal");

            migrationBuilder.RenameTable(
                name: "SavingGoal",
                newName: "SavingGoals");

            migrationBuilder.RenameTable(
                name: "MortgageGoal",
                newName: "MortgageGoals");

            migrationBuilder.RenameTable(
                name: "DebtGoal",
                newName: "DebtGoals");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavingGoals",
                table: "SavingGoals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MortgageGoals",
                table: "MortgageGoals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DebtGoals",
                table: "DebtGoals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_DebtGoals_DebtGoalId",
                table: "Budgets",
                column: "DebtGoalId",
                principalTable: "DebtGoals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_MortgageGoals_MortgageGoalId",
                table: "Budgets",
                column: "MortgageGoalId",
                principalTable: "MortgageGoals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_SavingGoals_SavingGoalId",
                table: "Budgets",
                column: "SavingGoalId",
                principalTable: "SavingGoals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_DebtGoals_DebtGoalId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_MortgageGoals_MortgageGoalId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_SavingGoals_SavingGoalId",
                table: "Budgets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavingGoals",
                table: "SavingGoals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MortgageGoals",
                table: "MortgageGoals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DebtGoals",
                table: "DebtGoals");

            migrationBuilder.RenameTable(
                name: "SavingGoals",
                newName: "SavingGoal");

            migrationBuilder.RenameTable(
                name: "MortgageGoals",
                newName: "MortgageGoal");

            migrationBuilder.RenameTable(
                name: "DebtGoals",
                newName: "DebtGoal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavingGoal",
                table: "SavingGoal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MortgageGoal",
                table: "MortgageGoal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DebtGoal",
                table: "DebtGoal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_DebtGoal_DebtGoalId",
                table: "Budgets",
                column: "DebtGoalId",
                principalTable: "DebtGoal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_MortgageGoal_MortgageGoalId",
                table: "Budgets",
                column: "MortgageGoalId",
                principalTable: "MortgageGoal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_SavingGoal_SavingGoalId",
                table: "Budgets",
                column: "SavingGoalId",
                principalTable: "SavingGoal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
