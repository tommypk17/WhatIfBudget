import { KeyValue } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { SavingGoalService } from '../../services/saving.goal.service';
import { SharedService } from '../../services/shared.service';

@Component({
  selector: 'app-saving-chart',
  templateUrl: './saving-chart.component.html',
  styleUrls: ['./saving-chart.component.scss']
})
export class SavingChartComponent implements OnInit {
  basicData: any
  basicOptions: any

  constructor(private savingGoalService: SavingGoalService, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.loadChartInfo();
    this.sharedService.chartReloadEmit.subscribe(() => {
      this.loadChartInfo();
    });

    this.basicOptions = {
      plugins: {
        legend: {
          labels: {
            color: '#495057'
          }
        }
      },
      scales: {
        x: {
          ticks: {
            color: '#495057'
          },
          grid: {
            color: '#ebedef'
          }
        },
        y: {
          ticks: {
            color: '#495057'
          },
          grid: {
            color: '#ebedef'
          }
        }
      }
    };
  }

  loadChartInfo(): void {
    let months: string[] = [];
    let values: number[] = [];

    this.savingGoalService.getBalanceOverTime(this.sharedService.budget.savingGoalId!).subscribe((res: KeyValue<number, number>[]) => {
      if (res) {
        if (res.length > 12) {
          res.forEach((v, i) => {
            if (v.key % 12 == 0) {
              months.push('Year ' + v.key / 12)
              values.push(v.value);
            } else {
              if (i == res.length - 1) {
                months.push('Year ' + (v.key / 12).toFixed(2))
                values.push(v.value);
              }
            }
          });
        } else {
          res.forEach((v) => {
            months.push('Month ' + v.key)
            values.push(v.value);
          });
        }

        this.basicData = {
          labels: months,
          datasets: [
            {
              label: '$',
              data: values,
              fill: false,
              backgroundColor: 'rgba(16,124,16,1)',
              tension: .4
            }
          ]
        };
      }
    });
  }
}
