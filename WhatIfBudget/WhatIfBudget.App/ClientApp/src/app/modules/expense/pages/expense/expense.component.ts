import { Component, OnInit } from '@angular/core';
import { ExpenseService } from '../../../../services/expense.service';
import { Expense } from '../../../../shared/models/expense';

@Component({
  selector: 'app-expense',
  templateUrl: './expense.component.html',
  styleUrls: ['./expense.component.scss']
})
export class ExpenseComponent implements OnInit {

  expenses: Expense[] = [];

  constructor(private expenseService: ExpenseService) { }

  ngOnInit(): void {
    this.expenseService.getExpenses().subscribe((res: Expense[]) => {
      if (res) this.expenses = res;
    });
  }

  expenseAdded(): void {

  }

}
