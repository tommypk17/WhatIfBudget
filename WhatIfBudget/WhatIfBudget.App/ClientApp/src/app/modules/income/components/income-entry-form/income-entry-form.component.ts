import { KeyValue } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { IncomeService } from '../../../../services/income.service';
import { SharedService } from '../../../../services/shared.service';
import { Income } from '../../../../shared/models/income';

@Component({
  selector: 'app-income-entry-form',
  templateUrl: './income-entry-form.component.html',
  styleUrls: ['./income-entry-form.component.scss']
})
export class IncomeEntryFormComponent implements OnInit {
  @Output('added') added: EventEmitter<void> = new EventEmitter();
  @Output('updated') updated: EventEmitter<void> = new EventEmitter();

  frequencies: KeyValue<number, string>[] = this.sharedService.frequencies;
  @Input('income') model: Income = new Income();

  constructor(private incomeService: IncomeService, private sharedService: SharedService) { }

  ngOnInit(): void {
  }

  onSubmit(event: NgForm): void {
    let income: Income = event.value as Income;
    income.budgetId = this.sharedService.budget.id;
    if (this.model.id && this.model.id > 0) {
      this.incomeService.updateIncome(this.model).subscribe((res: Income) => {
        this.updated.emit();
        this.model = new Income();
      });
    } else {
      this.incomeService.saveIncome(income).subscribe((res: Income) => {
        this.added.emit();
        this.model = new Income();
      });
    }
  }

}
