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
        if (res.length > 12) {
          res.forEach((v, i) => {
            if (v.key % 12 == 0) {
              months.push('Year ' + v.key / 12)
              balances.push(v.value);
            } else {
              if (i == res.length - 1) {
                months.push('Year ' + (v.key / 12).toFixed(2))
                balances.push(v.value);
              }
            }
          });
        } else {
          res.forEach((v) => {
            months.push('Month ' + v.key)
            balances.push(v.value);
          });
        }

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
