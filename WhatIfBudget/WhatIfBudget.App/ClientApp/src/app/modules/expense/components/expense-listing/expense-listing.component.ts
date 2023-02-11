import { KeyValue } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
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

  frequencies: KeyValue<number, string>[] = this.sharedService.frequencies;
  priorities: KeyValue<number, string>[] = this.sharedService.priorities;

  constructor(private expenseService: ExpenseService, private sharedService: SharedService) { }

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

}
