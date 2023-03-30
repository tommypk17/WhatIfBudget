import { Component, OnInit } from '@angular/core';
import { MortgageService } from '../../../../services/mortgage.service';
import { SharedService } from '../../../../services/shared.service';
import { MortgageTotals } from '../../../../shared/models/mortgage';

@Component({
  selector: 'app-mortgage',
  templateUrl: './mortgage.component.html',
  styleUrls: ['./mortgage.component.scss']
})
export class MortgageComponent implements OnInit {

  totalMortgageCost: number = 0;
  totalMortgageInterestCost: number = 0;
  totalMortgageYears: number = 0;
  totalMortgageMonths: number = 0;

  constructor(private mortgageService: MortgageService, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.updateMortgageTotals();
  }

  updateMortgage(): void {
    this.updateMortgageTotals();
  }

  updateMortgageTotals(): void {
    this.mortgageService.getMortgageTotals(this.sharedService.budget.mortgageGoalId!).subscribe((res: MortgageTotals) => {
      this.totalMortgageCost = res.totalCostToPayoff ?? 0;
      this.totalMortgageInterestCost = res.totalInterestAccrued ?? 0;
      let totalMonths = res.monthsToPayoff ?? 0;

      var years = Math.floor(totalMonths / 12);
      var months = totalMonths % 12;

      if (years) {
        this.totalMortgageYears = years;
      }
      if (months) {
        this.totalMortgageMonths = months;
      }
    })
  }

}
