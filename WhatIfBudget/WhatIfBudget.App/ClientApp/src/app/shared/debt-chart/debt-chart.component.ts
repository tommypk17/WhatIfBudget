import { KeyValue } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DebtService } from '../../services/debt.service';
import { SharedService } from '../../services/shared.service';

@Component({
  selector: 'app-debt-chart',
  templateUrl: './debt-chart.component.html',
  styleUrls: ['./debt-chart.component.scss']
})
export class DebtChartComponent implements OnInit {
  basicData: any;

  constructor(private debtService: DebtService, private sharedService: SharedService) { }

  ngOnInit(): void {
    let months: string[] = [];
    let balances: number[] = [];

    this.debtService.getDebtGoalBalanceOverTime(this.sharedService.budget.debtGoalId!).subscribe((res: KeyValue<number, number>[]) => {
      if (res) {
        res.forEach((v) => {
          months.push('Year ' + v.key)
          balances.push(v.value);
        });

        this.basicData = {
          labels: months,
          datasets: [
            {
              label: 'Balance',
              data: balances,
              fill: false,
              borderColor: 'red',
              tension: .4
            }
          ]
        };
      }
    });

  }
}
