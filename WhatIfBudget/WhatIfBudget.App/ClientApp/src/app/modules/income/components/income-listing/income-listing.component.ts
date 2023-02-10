import { Component, OnInit } from '@angular/core';
import { KeyValue } from '@angular/common';
import { IncomeService } from '../../../../services/income.service';
import { Income } from '../../../../shared/models/income';

@Component({
  selector: 'app-income-listing',
  templateUrl: './income-listing.component.html',
  styleUrls: ['./income-listing.component.scss']
})
export class IncomeListingComponent implements OnInit {

  frequencies: KeyValue<number, string>[] = [
    { key: 0, value: 'None' },
    { key: 1, value: 'Weekly' },
    { key: 2, value: 'Monthly' },
    { key: 3, value: 'Quarterly' },
    { key: 4, value: 'Yearly' },
  ]
  model: Income = new Income();

  constructor(private incomeService: IncomeService) { }

  ngOnInit(): void {
  }

}
