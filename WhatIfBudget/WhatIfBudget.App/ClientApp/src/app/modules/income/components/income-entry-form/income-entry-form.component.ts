import { KeyValue } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
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

  frequencies: KeyValue<number, string>[] = this.sharedService.frequencies;
  model: Income = new Income();

  constructor(private incomeService: IncomeService, private sharedService: SharedService) { }

  ngOnInit(): void {
  }

  onSubmit(event: NgForm): void {
    this.incomeService.saveIncome(event.value as Income).subscribe((res: Income) => {
      this.added.emit();
    });
  }

}
