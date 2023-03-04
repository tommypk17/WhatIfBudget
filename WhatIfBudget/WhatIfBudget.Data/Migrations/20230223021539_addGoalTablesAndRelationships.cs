using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatIfBudget.Data.Migrations
{
    public partial class addGoalTablesAndRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DebtGoal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtGoal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalBalance = table.Column<double>(type: "float", nullable: false),
                    AnnualReturnRate_Percent = table.Column<double>(type: "float", nullable: false),
                    YearsToMaturation = table.Column<int>(type: "int", nullable: false),
                    AnnualRaiseFactor_Percent = table.Column<double>(type: "float", nullable: false),
                    AdditionalBudgetAllocation = table.Column<double>(type: "float", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentGoals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MortgageGoal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalBalance = table.Column<double>(type: "float", nullable: false),
                    InterestRate_Percent = table.Column<double>(type: "float", nullable: false),
                    MonthlyPayment = table.Column<double>(type: "float", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MortgageGoal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SavingGoal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalBalance = table.Column<double>(type: "float", nullable: false),
                    AnnualReturnRate_Percent = table.Column<double>(type: "float", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingGoal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SavingGoalId = table.Column<int>(type: "int", nullable: false),
                    DebtGoalId = table.Column<int>(type: "int", nullable: false),
                    MortgageGoalId = table.Column<int>(type: "int", nullable: false),
                    InvestmentGoalId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_DebtGoal_DebtGoalId",
                        column: x => x.DebtGoalId,
                        principalTable: "DebtGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Budgets_InvestmentGoals_InvestmentGoalId",
                        column: x => x.InvestmentGoalId,
                        principalTable: "InvestmentGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Budgets_MortgageGoal_MortgageGoalId",
                        column: x => x.MortgageGoalId,
                        principalTable: "MortgageGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Budgets_SavingGoal_SavingGoalId",
                        column: x => x.SavingGoalId,
                        principalTable: "SavingGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetId = table.Column<int>(type: "int", nullable: false),
                    ExpenseId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetExpenses_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetExpenses_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetIncomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetId = table.Column<int>(type: "int", nullable: false),
                    IncomeId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetIncomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetIncomes_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetIncomes_Incomes_IncomeId",
                        column: x => x.IncomeId,
                        principalTable: "Incomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetExpenses_BudgetId",
                table: "BudgetExpenses",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetExpenses_ExpenseId",
                table: "BudgetExpenses",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetIncomes_BudgetId",
                table: "BudgetIncomes",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetIncomes_IncomeId",
                table: "BudgetIncomes",
                column: "IncomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_DebtGoalId",
                table: "Budgets",
                column: "DebtGoalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_InvestmentGoalId",
                table: "Budgets",
                column: "InvestmentGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_MortgageGoalId",
                table: "Budgets",
                column: "MortgageGoalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_SavingGoalId",
                table: "Budgets",
                column: "SavingGoalId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetExpenses");

            migrationBuilder.DropTable(
                name: "BudgetIncomes");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "DebtGoal");

            migrationBuilder.DropTable(
                name: "InvestmentGoals");

            migrationBuilder.DropTable(
                name: "MortgageGoal");

            migrationBuilder.DropTable(
                name: "SavingGoal");
        }
    }
}
