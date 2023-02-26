using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WhatIfBudget.Data.Models;

namespace WhatIfBudget.Data.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<SavingGoal> SavingGoals { get; set; }
        public DbSet<DebtGoal> DebtGoals { get; set; }
        public DbSet<MortgageGoal> MortgageGoals { get; set; }
        public DbSet<InvestmentGoal> InvestmentGoals { get; set; }
        public DbSet<Investment> Investments { get; set; }

        public DbSet<BudgetIncome> BudgetIncomes { get; set; }
        public DbSet<BudgetExpense> BudgetExpenses { get; set; }
        public DbSet<InvestmentGoalInvestment> InvestmentGoalInvestments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
