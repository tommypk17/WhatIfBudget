import { Component, OnInit } from '@angular/core';
import { IncomeService } from '../../../../services/income.service';
import { SharedService } from '../../../../services/shared.service';

@Component({
  selector: 'app-income-monthly-detail-section',
  templateUrl: './income-monthly-detail-section.component.html',
  styleUrls: ['./income-monthly-detail-section.component.scss']
})
export class IncomeMonthlyDetailSectionComponent implements OnInit {

  myMonthlyIncome: number = 0;

  constructor(private incomeService: IncomeService, private sharedService: SharedService) { }

  ngOnInit(): void {
    if (this.sharedService.budget.id) {
      this.incomeService.getMonthlyIncomeByBudgetId(this.sharedService.budget.id).subscribe((res: number) => {
        if (res) this.myMonthlyIncome = res;
      });
    }
  }
}
