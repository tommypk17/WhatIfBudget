import { Component, OnInit } from '@angular/core';
import { ExpenseService } from '../../../../services/expense.service';
import { SharedService } from '../../../../services/shared.service';
import { Expense } from '../../../../shared/models/expense';

@Component({
  selector: 'app-expense',
  templateUrl: './expense.component.html',
  styleUrls: ['./expense.component.scss']
})
export class ExpenseComponent implements OnInit {
  reloadGraph: boolean = true;
  expenses: Expense[] = [];

  constructor(private expenseService: ExpenseService, private sharedService: SharedService) { }

  ngOnInit(): void {
    if (this.sharedService.budget.id) {
      this.expenseService.getExpensesByBudgetId(this.sharedService.budget.id).subscribe((res: Expense[]) => {
        if (res) this.expenses = res;
      });
    }
  }

  updateExpenses(): void {
    this.reloadGraph = false;
    if (this.sharedService.budget.id) {
      this.expenseService.getExpensesByBudgetId(this.sharedService.budget.id).subscribe((res: Expense[]) => {
        if (res) this.expenses = res;
        this.reloadGraph = true;
      });
    }
  }

}
