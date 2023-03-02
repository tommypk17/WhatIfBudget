import { Component, OnInit } from '@angular/core';
import { InvestmentGoalService } from '../../../../services/investment.goal.service';
import { InvestmentService } from '../../../../services/investment.service';
import { SharedService } from '../../../../services/shared.service';
import { Investment } from '../../../../shared/models/investment';
import { InvestmentGoal } from '../../../../shared/models/investment-goal';

@Component({
  selector: 'app-investment',
  templateUrl: './investment.component.html',
  styleUrls: ['./investment.component.scss']
})
export class InvestmentComponent implements OnInit {

  investments: Investment[] = [];
  investmentGoal: InvestmentGoal = new InvestmentGoal();

  constructor(private investmentService: InvestmentService, private investmentGoalService: InvestmentGoalService, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.investmentService.getInvestmentsByGoalId(this.sharedService.budget.investmentGoalId ?? 0).subscribe((res: Investment[]) => {
      if (res) this.investments = res;
    });
    this.investmentGoalService.getInvestmentGoal(this.sharedService.budget.investmentGoalId ?? 0).subscribe((res: InvestmentGoal) => {
      if (res) this.investmentGoal = res;
    });
  }

  investmentAdded(): void {
    this.investmentService.getInvestmentsByGoalId(this.sharedService.budget.investmentGoalId ?? 0).subscribe((res: Investment[]) => {
      if (res) this.investments = res;
    });
  }

  investmentGoalUpdated(): void {
    //this.investmentGoalService.getInvestmentGoals(this.sharedService.budget.investmentGoalId ?? 0).subscribe((res: InvestmentGoal[]) => {
    //  if (res) this.investmentGoals = res;
    //});
  }
}
