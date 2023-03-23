import { Component, OnInit } from '@angular/core';
import { DebtService } from '../../../../services/debt.service';
import { SharedService } from '../../../../services/shared.service';
import { Debt } from '../../../../shared/models/debt';

@Component({
  selector: 'app-debt',
  templateUrl: './debt.component.html',
  styleUrls: ['./debt.component.scss']
})
export class DebtComponent implements OnInit {
  balanceAtTarget: number = 0;
  debts: Debt[] = [];

  constructor(private debtService: DebtService, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.debtService.getDebtsByGoalId(this.sharedService.budget.debtGoalId ?? 0).subscribe((res: Debt[]) => {
      if (res) this.debts = res;
    });
  }

  updateDebts(): void {
    this.debtService.getDebtsByGoalId(this.sharedService.budget.debtGoalId ?? 0).subscribe((res: Debt[]) => {
      if (res) this.debts = res;
    });
  }
}
