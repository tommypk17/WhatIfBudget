import { Component, OnInit } from '@angular/core';
import { DebtService } from '../../../../services/debt.service';
import { SharedService } from '../../../../services/shared.service';
import { Debt } from '../../../../shared/models/debt';
import { DebtTotals } from '../../../../shared/models/debt-goal';

@Component({
  selector: 'app-debt',
  templateUrl: './debt.component.html',
  styleUrls: ['./debt.component.scss']
})
export class DebtComponent implements OnInit {
  balanceAtTarget: number = 0;
  debts: Debt[] = [];

  totalDebtCost: number = 0;
  totalDebtInterestCost: number = 0;
  totalDebtYears: number = 0;
  totalDebtMonths: number = 0;

  constructor(private debtService: DebtService, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.debtService.getDebtsByGoalId(this.sharedService.budget.debtGoalId ?? 0).subscribe((res: Debt[]) => {
      if (res) this.debts = res;
    });
    this.debtService.getDebtTotals(this.sharedService.budget.debtGoalId!).subscribe((res: DebtTotals) => {
      let totalMonths = res.monthsToPayoff ?? 0;
      this.totalDebtCost = res.totalCostToPayoff ?? 0;
      this.totalDebtInterestCost = res.totalInterestAccrued ?? 0;

      var years = Math.floor(totalMonths / 12);
      var months = totalMonths % 12;

      if (years) {
        this.totalDebtYears = years;
      }
      if (months) {
        this.totalDebtMonths = months;
      }
    })
  }

  updateDebts(): void {
    this.debtService.getDebtsByGoalId(this.sharedService.budget.debtGoalId ?? 0).subscribe((res: Debt[]) => {
      if (res) this.debts = res;
    });
  }
}
