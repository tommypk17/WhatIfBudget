﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WhatIfBudget.Data.DAL;

#nullable disable

namespace WhatIfBudget.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WhatIfBudget.Data.Models.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DebtGoalId")
                        .HasColumnType("int");

                    b.Property<int>("InvestmentGoalId")
                        .HasColumnType("int");

                    b.Property<int>("MortgageGoalId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SavingGoalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DebtGoalId")
                        .IsUnique();

                    b.HasIndex("InvestmentGoalId");

                    b.HasIndex("MortgageGoalId")
                        .IsUnique();

                    b.HasIndex("SavingGoalId")
                        .IsUnique();

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.BudgetExpense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BudgetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpenseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.HasIndex("ExpenseId");

                    b.ToTable("BudgetExpenses");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.BudgetIncome", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BudgetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("IncomeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.HasIndex("IncomeId");

                    b.ToTable("BudgetIncomes");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.DebtGoal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("DebtGoals");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.Income", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.InvestmentGoal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("AdditionalBudgetAllocation")
                        .HasColumnType("float");

                    b.Property<double>("AnnualRaiseFactor_Percent")
                        .HasColumnType("float");

                    b.Property<double>("AnnualReturnRate_Percent")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalBalance")
                        .HasColumnType("float");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("YearsToMaturation")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("InvestmentGoals");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.MortgageGoal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<double>("InterestRate_Percent")
                        .HasColumnType("float");

                    b.Property<double>("MonthlyPayment")
                        .HasColumnType("float");

                    b.Property<double>("TotalBalance")
                        .HasColumnType("float");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MortgageGoals");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.SavingGoal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("AnnualReturnRate_Percent")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalBalance")
                        .HasColumnType("float");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SavingGoals");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.Budget", b =>
                {
                    b.HasOne("WhatIfBudget.Data.Models.DebtGoal", "DebtGoal")
                        .WithOne("Budget")
                        .HasForeignKey("WhatIfBudget.Data.Models.Budget", "DebtGoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhatIfBudget.Data.Models.InvestmentGoal", "InvestmentGoal")
                        .WithMany("Budgets")
                        .HasForeignKey("InvestmentGoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhatIfBudget.Data.Models.MortgageGoal", "MortgageGoal")
                        .WithOne("Budget")
                        .HasForeignKey("WhatIfBudget.Data.Models.Budget", "MortgageGoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhatIfBudget.Data.Models.SavingGoal", "SavingGoal")
                        .WithOne("Budget")
                        .HasForeignKey("WhatIfBudget.Data.Models.Budget", "SavingGoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DebtGoal");

                    b.Navigation("InvestmentGoal");

                    b.Navigation("MortgageGoal");

                    b.Navigation("SavingGoal");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.BudgetExpense", b =>
                {
                    b.HasOne("WhatIfBudget.Data.Models.Budget", "Budget")
                        .WithMany()
                        .HasForeignKey("BudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhatIfBudget.Data.Models.Expense", "Expense")
                        .WithMany("BudgetExpenses")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Budget");

                    b.Navigation("Expense");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.BudgetIncome", b =>
                {
                    b.HasOne("WhatIfBudget.Data.Models.Budget", "Budget")
                        .WithMany()
                        .HasForeignKey("BudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhatIfBudget.Data.Models.Income", "Income")
                        .WithMany("BudgetIncomes")
                        .HasForeignKey("IncomeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Budget");

                    b.Navigation("Income");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.DebtGoal", b =>
                {
                    b.Navigation("Budget");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.Expense", b =>
                {
                    b.Navigation("BudgetExpenses");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.Income", b =>
                {
                    b.Navigation("BudgetIncomes");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.InvestmentGoal", b =>
                {
                    b.Navigation("Budgets");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.MortgageGoal", b =>
                {
                    b.Navigation("Budget");
                });

            modelBuilder.Entity("WhatIfBudget.Data.Models.SavingGoal", b =>
                {
                    b.Navigation("Budget");
                });
#pragma warning restore 612, 618
        }
    }
}
