import { KeyValue } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { IncomeService } from '../../../../services/income.service';
import { Income } from '../../../../shared/models/income';

@Component({
  selector: 'app-example-form',
  templateUrl: './example-form.component.html',
  styleUrls: ['./example-form.component.scss']
})
export class ExampleFormComponent implements OnInit {
  frequencies: KeyValue<number, string>[] = [
    {key: 0, value: 'None'},
    {key: 1, value: 'Weekly'},
    {key: 2, value: 'Monthly'},
    {key: 3, value: 'Quarterly'},
    {key: 4, value: 'Yearly'},
  ]
  model: Income = new Income();

  constructor(private incomeService: IncomeService) { }

  ngOnInit(): void {
  }

  onSubmit(event: NgForm): void {
    this.incomeService.saveIncome(event.value as Income).subscribe((res: Income) => {
      console.log('Saved!')
    });
  }

}
