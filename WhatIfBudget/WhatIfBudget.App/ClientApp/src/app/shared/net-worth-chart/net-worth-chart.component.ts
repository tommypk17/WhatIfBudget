import { KeyValue } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BudgetService } from '../../services/budget.service';
import { SharedService } from '../../services/shared.service';
import { NetValue } from '../models/budget';

@Component({
  selector: 'app-net-worth-chart',
  templateUrl: './net-worth-chart.component.html',
  styleUrls: ['./net-worth-chart.component.scss']
})
export class NetWorthChartComponent implements OnInit {
  basicData: any;

  constructor(private budgetService: BudgetService, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.loadChartInfo();
  }

  loadChartInfo(): void {
    let months: string[] = [];
    let balances: number[] = [];

    this.budgetService.getNetWorthOverTime(this.sharedService.budget.id!).subscribe((res: NetValue) => {
      if (res) {
        if (res.balance.length > 12) {
          res.balance.forEach((v, i) => {
            if (v.key % 12 == 0) {
              months.push('Year ' + v.key / 12)
              balances.push(v.value);
            }
            else {
              if (i == res.balance.length - 1) {
                months.push('Year ' + (v.key / 12).toFixed(2))
                balances.push(v.value);
              }
            }
          });
        }
        else {
          res.balance.forEach((v) => {
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
