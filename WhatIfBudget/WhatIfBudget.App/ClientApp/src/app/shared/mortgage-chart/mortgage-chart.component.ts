import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-mortgage-chart',
  templateUrl: './mortgage-chart.component.html',
  styleUrls: ['./mortgage-chart.component.scss']
})
export class MortgageChartComponent implements OnInit {

  basicData: any;

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {

    this.basicData = {
      labels: ['0', '1', '2', '3', '4', '5'],
      datasets: [
        {
          label: 'Balance',
          data: [100, 85, 65, 40, 10, 0],
          fill: false,
          borderColor: 'blue',
          tension: .4
        },
        {
          label: 'Principal Paid',
          data: [0, 15, 35, 60, 90, 100],
          fill: false,
          borderColor: 'red',
          tension: .4
        },
        {
          label: 'Interest Paid',
          data: [0, 25, 45, 60, 70, 71],
          fill: false,
          borderColor: 'black',
          tension: .4
        }
      ]
    };

  }
}
