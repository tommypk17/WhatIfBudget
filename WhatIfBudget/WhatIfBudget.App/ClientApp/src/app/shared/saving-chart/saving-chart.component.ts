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
    let months: string[] = [];
    let values: number[] = []


    this.savingGoalService.getBalanceOverTime(this.sharedService.budget.savingGoalId!).subscribe((res: KeyValue<number, number>[]) => {
      if (res) {
        res.forEach((v) => {
          months.push('Month ' + v.key)
          values.push(v.value);
        });

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

}
