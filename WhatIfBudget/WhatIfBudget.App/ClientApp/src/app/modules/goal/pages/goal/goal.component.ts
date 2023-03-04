import { Component, OnInit } from '@angular/core';
import { InvestmentService } from '../../../../services/investment.service';
import { Investment } from '../../../../shared/models/investment';

@Component({
  selector: 'app-goal',
  templateUrl: './goal.component.html',
  styleUrls: ['./goal.component.scss']
})
export class GoalComponent implements OnInit {

  investments: Investment[] = [];

  constructor(private investmentService: InvestmentService) { }

  ngOnInit(): void {
    this.investmentService.getInvestments().subscribe((res: Investment[]) => {
      if (res) this.investments = res;
    });
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
