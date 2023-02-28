import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { KeyValue } from '@angular/common';
import { IncomeService } from '../../../../services/income.service';
import { Income } from '../../../../shared/models/income';
import { SharedService } from '../../../../services/shared.service';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-income-listing',
  templateUrl: './income-listing.component.html',
  styleUrls: ['./income-listing.component.scss']
})
export class IncomeListingComponent implements OnInit {
  @Input('incomes') model: Income[] = [];

  editModal: boolean = false;
  editIncome: Income | undefined;

  frequencies: KeyValue<number, string>[] = this.sharedService.frequencies;

  constructor(private confirmationService: ConfirmationService, private messageService: MessageService, private incomeService: IncomeService, private sharedService: SharedService) { }

  ngOnInit(): void {

  }

  displayFrequencies(type: number): string {
    let typeString: string | undefined = this.frequencies.find(x => x.key == type)?.value;
    return typeString ? typeString : 'N/A';
  }

  delete(event: Event, income: Income) {
    this.confirmationService.confirm({
      target: event.target!,
      message: 'Delete this item?',
      accept: () => {
        this.incomeService.deleteIncome(income).subscribe((res: Income) => {
          if (res) {
            this.refreshTable();
          }
        });
      }
    });
  }

  edit(event: Event, income: Income) {
    this.editIncome = Object.assign(new Income(), income);
    this.editModal = true
  }

  editComplete(): void {
    this.editIncome = undefined;
    this.editModal = false;

    this.refreshTable();
  }

  refreshTable(): void {
    this.incomeService.getIncomesByBudgetId(this.sharedService.budget.id!).subscribe((res: Income[]) => {
      if (res) this.model = res;
    });
  }
}
