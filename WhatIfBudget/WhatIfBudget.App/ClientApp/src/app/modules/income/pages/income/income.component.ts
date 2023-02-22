import { Component, OnInit } from '@angular/core';
import { IncomeService } from '../../../../services/income.service';
import { Income } from '../../../../shared/models/income';

@Component({
  selector: 'app-income',
  templateUrl: './income.component.html',
  styleUrls: ['./income.component.scss']
})
export class IncomeComponent implements OnInit {

  incomes: Income[] = [];

  constructor(private incomeService: IncomeService) { }

  ngOnInit(): void {
    this.incomeService.getIncomes().subscribe((res: Income[]) => {
      if (res) this.incomes = res;
    });
  }

  incomeAdded(): void {
    this.incomeService.getIncomes().subscribe((res: Income[]) => {
      if (res) this.incomes = res;
    });
  }
}
