import { Component, OnInit } from '@angular/core';
import { ChartModule } from 'primeng/chart';
import { ExpenseService } from '../../../../services/expense.service';
import { IncomeService } from '../../../../services/income.service';
import { SharedService } from '../../../../services/shared.service';


@Component({
  selector: 'app-expense-detail-section',
  templateUrl: './expense-detail-section.component.html',
  styleUrls: ['./expense-detail-section.component.scss']
})
export class ExpenseDetailSectionComponent implements OnInit {

  myMonthlyExpense: number = 50;
  myMonthlyIncome: number = 100;
  myMonthlyNeed: number = 30;
  myMonthlyWant: number = 20;

  data: any;
  chartOptions: any;
  
  constructor(private expenseService: ExpenseService, private sharedService: SharedService, private incomeService: IncomeService) { }

  ngOnInit() {
    this.data = {
      labels: ['Needs', 'Wants', 'Remaining Income'],
      datasets: [
        {
          data: [this.myMonthlyNeed, this.myMonthlyWant, (this.myMonthlyIncome - this.myMonthlyNeed - this.myMonthlyWant)],
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

  monthlyExpense(): void {
    if (this.sharedService.budget.id) {
      this.expenseService.getMonthlyExpenseByBudgetId(this.sharedService.budget.id).subscribe((res: number) => {
        if (res) this.myMonthlyExpense = res;
      });
    }
  }

  monthlyNeed(): void {
    if (this.sharedService.budget.id) {
      this.expenseService.GetMonthlyNeeds(this.sharedService.budget.id).subscribe((res: number) => {
        if (res) this.myMonthlyNeed = res;
      });
    }
  }

  monthlyWant(): void {
    if (this.sharedService.budget.id) {
      this.expenseService.GetMonthlyWants(this.sharedService.budget.id).subscribe((res: number) => {
        if (res) this.myMonthlyWant = res;
      });
    }
  }

  monthlyIncome(): void {
    if (this.sharedService.budget.id) {
      this.incomeService.getMonthlyIncomeByBudgetId(this.sharedService.budget.id).subscribe((res: number) => {
        if (res) this.myMonthlyIncome = res;
      });
    }
  }
}
