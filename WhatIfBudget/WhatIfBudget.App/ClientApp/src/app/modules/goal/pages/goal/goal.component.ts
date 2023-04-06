import { Component, OnInit } from '@angular/core';
import { BudgetService } from '../../../../services/budget.service';
import { InvestmentService } from '../../../../services/investment.service';
import { SharedService } from '../../../../services/shared.service';
import { Budget } from '../../../../shared/models/budget';
import { Investment } from '../../../../shared/models/investment';

@Component({
  selector: 'app-goal',
  templateUrl: './goal.component.html',
  styleUrls: ['./goal.component.scss']
})
export class GoalComponent implements OnInit {

  private _budget: Budget | undefined = this.sharedService.budget;
  investments: Investment[] = [];
  availableMonthlyNet: number = 0;

  constructor(private sharedService: SharedService, private budgetService: BudgetService, private investmentService: InvestmentService) { }

  ngOnInit(): void {
    this.investmentService.getInvestments().subscribe((res: Investment[]) => {
      if (res) this.investments = res;
    });
    this.sharedService.budgetLoadedEmit.subscribe(() => {
      if (this.sharedService.budgetLoaded) this._budget = this.sharedService.budget;
    })
    if (this.sharedService.budget.id) {
      this.budgetService.getAvailableFreeCash(this.sharedService.budget.id).subscribe((res: number) => {
        if (res) this.availableMonthlyNet = res;
      });
    }
  }

  investmentAdded(): void {
    this.investmentService.getInvestments().subscribe((res: Investment[]) => {
      if (res) this.investments = res;
    });
  }

  investmentGoalAdded(): void {
    this.investmentService.getInvestments().subscribe((res: Investment[]) => {
      if (res) this.investments = res;
    });
  }

}
