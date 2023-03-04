import { Component, OnInit } from '@angular/core';
import { ExpenseService } from '../../../../services/expense.service';
import { IncomeService } from '../../../../services/income.service';
import { SharedService } from '../../../../services/shared.service';


@Component({
  selector: 'app-expense-detail-section',
  templateUrl: './expense-detail-section.component.html',
  styleUrls: ['./expense-detail-section.component.scss']
})
export class ExpenseDetailSectionComponent implements OnInit {

  private _monthlyExpense: number = 0;
  private _monthlyIncome: number = 0;
  private _monthlyNeed: number = 0;
  private _monthlyWant: number = 0;

  data: any;
  chartOptions: any;
  
  constructor(private expenseService: ExpenseService, private sharedService: SharedService, private incomeService: IncomeService) { }

  ngOnInit(): void {
    // Monthly expenses by budget id
    if (this.sharedService.budget.id) {
      this.expenseService.getMonthlyExpenseByBudgetId(this.sharedService.budget.id).subscribe((res: number) => {
        if (res) this._monthlyExpense = res;
        this.data = this.refreshData();
      });
    }

    // Monthly needs
    if (this.sharedService.budget.id) {
      this.expenseService.GetMonthlyNeeds(this.sharedService.budget.id).subscribe((res: number) => {
        if (res) this._monthlyNeed = res;
        this.data = this.refreshData();
      });
    }

    // Monthly wants
    if (this.sharedService.budget.id) {
      this.expenseService.GetMonthlyWants(this.sharedService.budget.id).subscribe((res: number) => {
        if (res) this._monthlyWant = res;
        this.data = this.refreshData();
      });
    }

    // Monthly income by budget id
    if (this.sharedService.budget.id) {
      this.incomeService.getMonthlyIncomeByBudgetId(this.sharedService.budget.id).subscribe((res: number) => {
        if (res) this._monthlyIncome = res;
        this.data = this.refreshData();
      });
    }
  }

  private refreshData(): any {
    return {
      labels: ['Needs', 'Wants', 'Remaining Income'],
      datasets: [
        {
          data: [this._monthlyNeed, this._monthlyWant, (this._monthlyIncome - this._monthlyNeed - this._monthlyWant)],
          backgroundColor: [
            "red",
            "blue",
            "green",
          ],
          hoverBackgroundColor: [
            "darkred",
            "darkblue",
            "darkgreen",
          ]
        }
      ]
    };
  }

}
