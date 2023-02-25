import { Component, OnInit } from '@angular/core';
import { IncomeService } from '../../../../services/income.service';
import { SharedService } from '../../../../services/shared.service';
import { Income } from '../../../../shared/models/income';

@Component({
  selector: 'app-income',
  templateUrl: './income.component.html',
  styleUrls: ['./income.component.scss']
})
export class IncomeComponent implements OnInit {

  incomes: Income[] = [];

  constructor(private incomeService: IncomeService, private sharedService: SharedService) { }

  ngOnInit(): void {
    if (this.sharedService.budget.id) {
      this.incomeService.getIncomesByBudgetId(this.sharedService.budget.id).subscribe((res: Income[]) => {
        if (res) this.incomes = res;
      });
    }
  }

  incomeAdded(): void {
    if (this.sharedService.budget.id) {
      this.incomeService.getIncomesByBudgetId(this.sharedService.budget.id).subscribe((res: Income[]) => {
        if (res) this.incomes = res;
      });
    }
  }
}
