import { Component, OnInit } from '@angular/core';
import { BudgetService } from '../../services/budget.service';
import { SharedService } from '../../services/shared.service';
import { NetValue } from '../models/budget';
import { default as Annotation } from 'chartjs-plugin-annotation';
require('chartjs-plugin-annotation');

@Component({
  selector: 'app-net-worth-chart',
  templateUrl: './net-worth-chart.component.html',
  styleUrls: ['./net-worth-chart.component.scss']
})
export class NetWorthChartComponent implements OnInit {
  basicData: any;
  basicOptions: any;

  constructor(private budgetService: BudgetService, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.loadChartInfo();
  }

  loadChartInfo(): void {
    let months: string[] = [];
    let balances: number[] = [];
    let debtGoalMonth: number | undefined;
    let mortgageGoalMonth: number | undefined;
    let savingGoalMonth: number | undefined;

    this.budgetService.getNetWorthOverTime(this.sharedService.budget.id!).subscribe((res: NetValue) => {
      if (res) {
        debtGoalMonth = res.debtGoalMonth! / 12;
        mortgageGoalMonth = res.mortgageGoalMonth! / 12;
        savingGoalMonth = res.savingGoalMonth! / 12;

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
              label: 'Debt Payoff',
              borderColor: 'red',
              tension: .4
            },
            {
              label: 'Mortgage Payoff',
              borderColor: 'blue',
              tension: .4
            },
            {
              label: 'Savings Finish',
              borderColor: 'green',
              tension: .4
            },
            {
              label: 'Balance',
              data: balances,
              fill: false,
              borderColor: 'black',
              tension: .4
            },
          ]
        };

        this.basicOptions = {
          plugins: {
            annotation: {
              annotations: {
                line: {
                  type: 'line',
                  xMin: debtGoalMonth,
                  xMax: debtGoalMonth,
                  borderColor: 'red',
                  borderWidth: 1,
                },
                line1: {
                  type: 'line',
                  xMin: mortgageGoalMonth,
                  xMax: mortgageGoalMonth,
                  borderColor: 'blue',
                  borderWidth: 1,
                },
                line2: {
                  type: 'line',
                  xMin: savingGoalMonth,
                  xMax: savingGoalMonth,
                  borderColor: 'green',
                  borderWidth: 1,
                },
              }
            }
          }
        }
      }
    });
  }

}
