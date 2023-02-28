import { KeyValue } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ExpenseService } from '../../../../services/expense.service';
import { SharedService } from '../../../../services/shared.service';
import { Expense } from '../../../../shared/models/expense';

@Component({
  selector: 'app-expense-entry-form',
  templateUrl: './expense-entry-form.component.html',
  styleUrls: ['./expense-entry-form.component.scss']
})
export class ExpenseEntryFormComponent implements OnInit {
  @Output('added') added: EventEmitter<void> = new EventEmitter();
  @Output('updated') updated: EventEmitter<void> = new EventEmitter();

  frequencies: KeyValue<number, string>[] = this.sharedService.frequencies;
  priorities: KeyValue<number, string>[] = this.sharedService.priorities;
  @Input('expense') model: Expense = new Expense();

  constructor(private expenseService: ExpenseService, private sharedService: SharedService) { }

  ngOnInit(): void {
  }

  onSubmit(event: NgForm): void {
    let expense: Expense = this.model as Expense;
    expense.budgetId = this.sharedService.budget.id;
    if (this.model.id && this.model.id > 0) {
      this.expenseService.updateExpense(expense).subscribe((res: Expense) => {
        this.updated.emit();
        this.model = new Expense();
      });
    } else {
      this.expenseService.saveExpense(expense).subscribe((res: Expense) => {
        this.added.emit();
        this.model = new Expense();
      });
    }
  }

}
