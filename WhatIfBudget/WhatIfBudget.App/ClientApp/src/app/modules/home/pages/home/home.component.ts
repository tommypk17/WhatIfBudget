import { Component, OnInit } from '@angular/core';
import { ExpenseService } from '../../../../services/expense.service';
import { IncomeService } from '../../../../services/income.service';
import { SharedService } from '../../../../services/shared.service';
import { Budget } from '../../../../shared/models/budget';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  myMonthlyIncome: number = 0;
  myMonthlyExpenses: number = 0;

  private _budget: Budget | undefined = this.sharedService.budget;

  constructor(private sharedService: SharedService, private incomeService: IncomeService, private expenseService: ExpenseService) { }

  ngOnInit(): void {
    this.sharedService.budgetLoadedEmit.subscribe(() => {
      if (this.sharedService.budgetLoaded) this._budget = this.sharedService.budget;
    })
    if (this.sharedService.budget.id) {
      this.incomeService.getMonthlyIncomeByBudgetId(this.sharedService.budget.id).subscribe((res: number) => {
        if (res) this.myMonthlyIncome = res;
      });
    }
    if (this.sharedService.budget.id) {
      this.expenseService.getMonthlyExpenseByBudgetId(this.sharedService.budget.id).subscribe((res: number) => {
        if (res) this.myMonthlyExpenses = res;
      });
    }
  }

  get budget(): Budget | undefined {
    return this._budget;
  }

}
