import { KeyValue } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ExpenseService } from '../../../../services/expense.service';
import { SharedService } from '../../../../services/shared.service';
import { Expense } from '../../../../shared/models/expense';

@Component({
  selector: 'app-expense-listing',
  templateUrl: './expense-listing.component.html',
  styleUrls: ['./expense-listing.component.scss']
})
export class ExpenseListingComponent implements OnInit {
  @Input('expenses') model: Expense[] = [];

  editModal: boolean = false;
  editExpense: Expense | undefined;

  frequencies: KeyValue<number, string>[] = this.sharedService.frequencies;
  priorities: KeyValue<number, string>[] = this.sharedService.priorities;

  constructor(private confirmationService: ConfirmationService, private messageService: MessageService, private expenseService: ExpenseService, private sharedService: SharedService) { }

  ngOnInit(): void {

  }

  displayFrequencies(type: number): string {
    let typeString: string | undefined = this.frequencies.find(x => x.key == type)?.value;
    return typeString ? typeString : 'N/A';
  }

  displayPriorities(type: number): string {
    let typeString: string | undefined = this.priorities.find(x => x.key == type)?.value;
    return typeString ? typeString : 'N/A';
  }

  delete(event: Event, expense: Expense) {
    this.confirmationService.confirm({
      target: event.target!,
      message: 'Delete this item?',
      accept: () => {
        this.expenseService.deleteExpense(expense).subscribe((res: Expense) => {
          if (res) {
            this.messageService.add({ severity: 'success', summary: 'Expense Deleted' });
            this.refreshTable();
          }
          else this.messageService.add({ severity: 'error', summary: 'Expense Delete Failed' });
        });
      }
    });
  }

  edit(event: Event, expense: Expense) {
    this.editExpense = Object.assign(new Expense(), expense);
    this.editModal = true
  }

  editComplete(): void {
    this.editExpense = undefined;
    this.editModal = false;

    this.refreshTable();
  }

  refreshTable(): void {
    this.expenseService.getExpenses().subscribe((res: Expense[]) => {
      if (res) this.model = res;
    });
  }

}
