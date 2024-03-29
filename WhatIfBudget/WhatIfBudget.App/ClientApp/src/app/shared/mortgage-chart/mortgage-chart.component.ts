import { KeyValue } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MortgageService } from '../../services/mortgage.service';
import { SharedService } from '../../services/shared.service';

@Component({
  selector: 'app-mortgage-chart',
  templateUrl: './mortgage-chart.component.html',
  styleUrls: ['./mortgage-chart.component.scss']
})
export class MortgageChartComponent implements OnInit {
  basicData: any;

  constructor(private mortgageService: MortgageService, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.loadChartInfo();
    this.sharedService.chartReloadEmit.subscribe((type: string) => {
      if (type == 'mortgage')
        this.loadChartInfo();
    });
  }

  loadChartInfo(): void {
    let months: string[] = [];
    let balances: number[] = [];
    let principalPaid: number[] = [];
    let interestPaid: number[] = [];

    this.mortgageService.getMortgageAmortization(this.sharedService.budget.mortgageGoalId!).subscribe((res: KeyValue<number, number[]>[]) => {
      if (res) {
        if (res.length > 12) {
          res.forEach((v, i) => {
            if (v.key % 12 == 0) {
              months.push('Year ' + v.key / 12)
              balances.push(v.value[0]);
              principalPaid.push(v.value[1]);
              interestPaid.push(v.value[2]);
            }
            else {
              if (i == res.length - 1) {
                months.push('Year ' + (v.key / 12).toFixed(2))
                balances.push(v.value[0]);
                principalPaid.push(v.value[1]);
                interestPaid.push(v.value[2]);
              }
            }
          });
        }
        else {
          res.forEach((v) => {
            months.push('Month ' + v.key)
            balances.push(v.value[0]);
            principalPaid.push(v.value[1]);
            interestPaid.push(v.value[2]);
          });
        }

        this.basicData = {
          labels: months,
          datasets: [
            {
              label: 'Balance',
              data: balances,
              fill: false,
              borderColor: 'blue',
              tension: .4
            },
            {
              label: 'Principal Paid',
              data: principalPaid,
              fill: false,
              borderColor: 'red',
              tension: .4
            },
            {
              label: 'Interest Paid',
              data: interestPaid,
              fill: false,
              borderColor: 'black',
              tension: .4
            }
          ]
        };
      }
    });
  }
}
