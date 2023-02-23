import { Component, OnInit } from '@angular/core';
import { InvestmentGoalService } from '../../../../services/investment.goal.service';
import { InvestmentService } from '../../../../services/investment.service';
import { Investment } from '../../../../shared/models/investment';
import { InvestmentGoal } from '../../../../shared/models/investment-goal';

@Component({
  selector: 'app-investment',
  templateUrl: './investment.component.html',
  styleUrls: ['./investment.component.scss']
})
export class InvestmentComponent implements OnInit {

  investments: Investment[] = [];
  investmentGoals: InvestmentGoal[] = [];

  constructor(private investmentService: InvestmentService, private investmentGoalService: InvestmentGoalService) { }

  ngOnInit(): void {
    this.investmentService.getInvestments().subscribe((res: Investment[]) => {
      if (res) this.investments = res;
    });
    this.investmentGoalService.getInvestmentGoals().subscribe((res: InvestmentGoal[]) => {
      if (res) this.investmentGoals = res;
    });
  }

  investmentAdded(): void {
    this.investmentService.getInvestments().subscribe((res: Investment[]) => {
      if (res) this.investments = res;
    });
  }

  investmentGoalAdded(): void {
    this.investmentGoalService.getInvestmentGoals().subscribe((res: InvestmentGoal[]) => {
      if (res) this.investmentGoals = res;
    });
  }
}
